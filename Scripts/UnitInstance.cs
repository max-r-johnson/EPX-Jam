using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class UnitInstance
{
	public Game game {get {return GameManager.Game;} }
	public Unit unitType { get; set; }
	public Node2D correspondingNode { get; set; }
	public int health { get; set; }
	public int attackSpeed { get; set; }
	public int attack { get; set; }
	public bool isEnemy { get; set; }

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
		return isEnemy
			? game.subjects.unitInstances[game.subjects.unitTypes[0]][0]
			: game.enemySubjects.unitInstances[game.enemySubjects.unitTypes[0]][0];
	}

	public async Task moveToEnemy(UnitInstance enemy)
	{
		if (enemy == null || enemy.health <= 0)
			enemy = findTarget();

		float speed = unitType.stats["movement speed"] * 50f;

		while (enemy.health > 0)
		{
			Vector2 currentPosition = correspondingNode.Position;
			Vector2 enemyPosition = enemy.correspondingNode.Position;

			float step = speed * (float)correspondingNode.GetProcessDeltaTime();
			Vector2 nextPosition = currentPosition.MoveToward(enemyPosition, step);
			correspondingNode.Position = nextPosition;

			if (currentPosition.DistanceTo(enemyPosition) < 1f)
			{
				await attackEnemy(enemy);
				break;
			}

			await correspondingNode.ToSignal(correspondingNode.GetTree(), "process_frame");
		}
	}

	public async Task attackEnemy(UnitInstance enemy)
	{
		while (enemy.health > 0)
		{
			GD.Print("take damage");
			enemy.health -= attack;
			float delaySeconds = 1f / attackSpeed;
			await correspondingNode.ToSignal(correspondingNode.GetTree().CreateTimer(delaySeconds), "timeout");
		}
		enemy.die();
	}

	public void die()
	{
		GD.Print("died");
		// temporarily remove instance - copy instances before/after battle
	}
}
