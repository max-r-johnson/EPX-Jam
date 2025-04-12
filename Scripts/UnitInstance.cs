using Godot;
using System;
using System.Collections.Generic;

public partial class UnitInstance
{
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
