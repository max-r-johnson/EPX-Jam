using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public partial class UnitInstance
{
	public Game game {get {return GameManager.Game;} }
	public Unit unitType { get; set; }
	public Node2D correspondingNode { get; set; }
	public float health { get; set; }
	public float attackSpeed { get; set; }
	public float attack { get; set; }
	public bool isEnemy { get; set; }
	public const float UNIT_RANGE = 20f;

	public UnitInstance(Unit unitType, Node2D correspondingNode)
	{
		this.unitType = unitType;
		this.correspondingNode = correspondingNode;
		health = unitType.stats["health"];
		attackSpeed = unitType.stats["attack speed"];
		attack = unitType.stats["attack"];
	}

	public UnitInstance findTarget()
	{
		GD.Print("Finding target");
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

	public async Task moveToEnemy(UnitInstance enemy)
	{
		float speed = unitType.stats["movement speed"] * 50f;

		while (health > 0)
		{
			if (enemy == null || enemy.health <= 0)
			{
				enemy = findTarget();
				if (enemy == null || enemy.health <= 0)
					return;
			}

			while (enemy.health > 0)
			{
				Vector2 currentPosition = correspondingNode.Position;
				Vector2 enemyPosition = enemy.correspondingNode.Position;

				var potentialNewTarget = findTarget();
				if (potentialNewTarget != null && potentialNewTarget != enemy && potentialNewTarget.health > 0)
				{
					float newDistance = currentPosition.DistanceTo(potentialNewTarget.correspondingNode.Position);
					float currentDistance = currentPosition.DistanceTo(enemy.correspondingNode.Position);
					if (newDistance + 5f < currentDistance)
					{
						GD.Print("Switching to closer target.");
						enemy = potentialNewTarget;
						break;
					}
				}

				float step = speed * (float)correspondingNode.GetProcessDeltaTime();
				Vector2 nextPosition = currentPosition.MoveToward(enemyPosition, step);
				correspondingNode.Position = nextPosition;

				if (currentPosition.DistanceTo(enemyPosition) < UNIT_RANGE)
				{
					await attackEnemy(enemy);
					break;
				}

				await correspondingNode.ToSignal(correspondingNode.GetTree(), "process_frame");
			}
		}
	}

	public async Task attackEnemy(UnitInstance enemy)
	{
		while (health > 0 && enemy != null && enemy.health > 0)
		{
			float distance = correspondingNode.Position.DistanceTo(enemy.correspondingNode.Position);
			if (distance > UNIT_RANGE)
			{
				GD.Print("Enemy out of range, chasing again.");
				break;
			}
			enemy.health -= attack;
			GD.Print($"{(isEnemy ? "Enemy" : "Ally")} attacked {(enemy.isEnemy ? "enemy" : "unit")}! Target health: {enemy.health}");

			if (enemy.health <= 0)
			{
				enemy.die();
				return;
			}

			float delaySeconds = 1f / attackSpeed;
			await correspondingNode.ToSignal(correspondingNode.GetTree().CreateTimer(delaySeconds), "timeout");
		}
	}

	public void die()
	{
		(isEnemy ? game.enemySubjects.unitInstances[unitType] : game.subjects.unitInstances[unitType]).Remove(this);
		correspondingNode.QueueFree();
		correspondingNode = null;
		GD.Print(isEnemy ? "Enemy died" : "Your unit died");
	}
}
