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
		return game.enemySubjects.unitInstances[game.enemySubjects.unitTypes[0]][0];
	}

	public async Task moveToEnemy(UnitInstance unitInstance)
	{
		while (health > 0)
		{
			Vector2 enemyPosition = unitInstance.correspondingNode.Position;

			if (correspondingNode.Position == enemyPosition)
			{
				GD.Print("attacking");
				break;
			}

			Tween tween = correspondingNode.GetTree().CreateTween();
			Vector2 currentPosition = correspondingNode.Position;
			float distance = currentPosition.DistanceTo(enemyPosition);
			float speed = unitType.stats["movement speed"] * 50;
			float duration = distance / speed;

			tween.TweenProperty(correspondingNode, "position", enemyPosition, duration)
				.SetTrans(Tween.TransitionType.Linear)
				.SetEase(Tween.EaseType.InOut);

			await correspondingNode.ToSignal(tween, "finished");
		}
	}

	public void attackEnemy(UnitInstance unitInstance)
	{
		unitInstance.health -= attack;
		if (unitInstance.health <= 0)
		{
			unitInstance.die();
		}
	}

	public void die()
	{
		// temporarily remove instance - copy instances before/after battle
	}
}
