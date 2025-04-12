using Godot;
using System;
using System.Collections.Generic;

public partial class UnitInstance
{
	public Unit unitType { get; set; }
	public Node2D correspondingNode { get; set; }
	public int health { get; set; }
	public UnitInstance(Unit unitType, Node2D correspondingNode)
	{
		this.unitType = unitType;
		this.correspondingNode = correspondingNode;
		health = unitType.stats["health"];
	}
}
