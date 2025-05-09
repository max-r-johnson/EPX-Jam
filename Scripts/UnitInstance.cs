using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

public partial class UnitInstance
{
	public Game game { get { return GameManager.Game; } }
	public Unit unitType { get; set; }
	public Node2D correspondingNode { get; set; }
	public float health { get; set; }
	public bool isEnemy { get; set; }
	public const float UNIT_RANGE = 20f;

	public UnitInstance(Unit unitType, Node2D correspondingNode)
	{
		this.unitType = unitType;
		this.correspondingNode = correspondingNode;
	}

	public UnitInstance findTarget()
	{
		List<UnitInstance> enemies = isEnemy
			? game.subjects.unitInstances.SelectMany(kvp => kvp.Value).ToList()
			: game.enemySubjects.unitInstances.SelectMany(kvp => kvp.Value).ToList();

		Vector2 myPos = correspondingNode.Position;

		UnitInstance closest = null;
		float closestDistance = float.MaxValue;

		foreach (var enemy in enemies)
		{
			if (enemy.health <= 0) continue;

			float distance = myPos.DistanceTo(enemy.correspondingNode.Position);
			if (distance < closestDistance)
			{
				closestDistance = distance;
				closest = enemy;
			}
		}

		return closest;
	}

	public async Task moveToEnemy(UnitInstance enemy, CancellationToken token)
	{
		float speed = unitType.stats["movement speed"] * unitType.statModifiers["movement speed"] * (isEnemy ? game.enemyGlobalModifiers["movement speed"] : game.globalModifiers["movement speed"]) * 50f;

		while (health > 0)
		{
			if (token.IsCancellationRequested)
			{
				return;
			}

			if (enemy == null || enemy.health <= 0)
			{
				enemy = findTarget();
				if (enemy == null || enemy.health <= 0)
				{
					return;
				}
			}

			Vector2 currentPosition = correspondingNode.Position;
			Vector2 enemyPosition = enemy.correspondingNode.Position;

			var potentialNewTarget = findTarget();
			if (potentialNewTarget != null && potentialNewTarget != enemy && potentialNewTarget.health > 0)
			{
				float newDistance = currentPosition.DistanceTo(potentialNewTarget.correspondingNode.Position);
				float currentDistance = currentPosition.DistanceTo(enemyPosition);
				if (newDistance + 5f < currentDistance)
				{
					enemy = potentialNewTarget;
					continue;
				}
			}

			float step = speed * (float)correspondingNode.GetProcessDeltaTime();
			Vector2 nextPosition = currentPosition.MoveToward(enemyPosition, step);
			correspondingNode.Position = nextPosition;

			if (nextPosition.DistanceTo(enemyPosition) < UNIT_RANGE)
			{
				await attackEnemy(enemy, token);
				continue;
			}

			bool frameWaited = await AwaitWithCancellation(
				correspondingNode.ToSignal(correspondingNode.GetTree(), "process_frame"), token);
			if (!frameWaited)
			{
				return;
			}
		}
	}

	public async Task attackEnemy(UnitInstance enemy, CancellationToken token)
	{
		while (health > 0 && enemy != null && enemy.health > 0)
		{
			if (token.IsCancellationRequested)
			{
				return;
			}

			float distance = correspondingNode.Position.DistanceTo(enemy.correspondingNode.Position);
			if (distance > UNIT_RANGE)
			{
				break;
			}

			enemy.health -= unitType.stats["attack"] * unitType.statModifiers["attack"] * (isEnemy ? game.enemyGlobalModifiers["attack"] : game.globalModifiers["attack"]);

			if (enemy.health <= 0)
			{
				enemy.die();
				break;
			}

			float delaySeconds = 1f / (unitType.stats["attack speed"] * unitType.statModifiers["attack speed"] * (isEnemy ? game.enemyGlobalModifiers["attack speed"] : game.globalModifiers["attack speed"]));
			var timer = correspondingNode.GetTree().CreateTimer(delaySeconds);
			bool waited = await AwaitWithCancellation(timer.ToSignal(timer, "timeout"), token);
			if (!waited)
			{
				return;
			}
		}
		float finalDelaySeconds = 1f / (unitType.stats["attack speed"] * unitType.statModifiers["attack speed"] * (isEnemy ? game.enemyGlobalModifiers["attack speed"] : game.globalModifiers["attack speed"]));
		var tree = (correspondingNode != null) ? correspondingNode.GetTree() : game.currentNode.GetTree();
		var finalTimer = tree.CreateTimer(finalDelaySeconds);
		bool finalWaited = await AwaitWithCancellation(finalTimer.ToSignal(finalTimer, "timeout"), token);
		if (!finalWaited)
		{
			return;
		}
	}

	public void die()
	{
		(isEnemy ? game.enemySubjects.unitInstances[unitType] : game.subjects.unitInstances[unitType]).Remove(this);
		correspondingNode.QueueFree();
		correspondingNode = null;
	}
	public static async Task<bool> AwaitWithCancellation(SignalAwaiter signalAwaiter, CancellationToken token)
	{
		var tcs = new TaskCompletionSource<bool>();

		using (token.Register(() => tcs.TrySetResult(false)))
		{
			var awaitSignal = AwaitSignal(signalAwaiter);
			var completedTask = await Task.WhenAny(awaitSignal, tcs.Task);
			return completedTask == awaitSignal;
		}
	}

	private static async Task AwaitSignal(SignalAwaiter awaiter)
	{
		await awaiter;
	}
}
