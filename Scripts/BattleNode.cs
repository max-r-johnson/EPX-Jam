using Godot;
using System;

public partial class BattleNode : Node
{
	public override void _Ready()
	{
		instantiateUnits(GameManager.Game.subjects);
	}

	public void instantiateUnits(Subjects subjects)
	{
		foreach (Unit unit in subjects.units)
		{
			foreach (int i in GD.Range(subjects.unitQuantities[unit]))
			{
				var unitScene = GD.Load<PackedScene>("res://Scenes/Unit.tscn");
				var unitInstance = unitScene.Instantiate();
				unitInstance.Name = unit.name;
				AddChild(unitInstance);
			}
		}
	}
}
