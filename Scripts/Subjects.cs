using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Subjects
{
	public Dictionary<Unit, int> unitQuantities { get; set; }
	public List<Unit> unitTypes { get; set; }
	public Game game {get {return GameManager.Game;}}
	public List<UnitInstance> unitInstances { get; set; }
	public bool isEnemy { get; set; }
	public Subjects(List<Unit> units)
	{
		unitTypes = units;
		unitQuantities = units.ToDictionary(unit => unit, unit => unit.quantity);
		unitInstances = [];
	}

	public void incLives(int amount)
	{
		var currentWeights = new Dictionary<Unit, int>(unitQuantities);

		int total = currentWeights.Values.Sum();

		foreach (int i in GD.Range(amount))
		{
			int randomValue = GD.RandRange(0, total-1);
			int cumulative = 0;

			foreach (var pair in currentWeights)
			{
				cumulative += pair.Value;
				if (randomValue < cumulative)
				{
					addUnitInstance(pair.Key);
					break;
				}
			}
		}
	}

	// Don't base off of existing weights, because there should be a larger chance the smaller group decrements after awhile
	public void decLives(int amount)
	{
		foreach (int i in GD.Range(amount))
		{
			int total = unitQuantities.Values.Sum();
			if (total == 0) break;

			int randomValue = GD.RandRange(0, total - 1);
			int cumulative = 0;

			foreach (var pair in unitQuantities)
			{
				cumulative += pair.Value;
				if (randomValue < cumulative)
				{
					removeUnitInstance(pair.Key);
					break;
				}
			}
		}
	}

	public void instantiateUnits()
	{
		foreach (Unit unit in unitTypes)
		{
			foreach (int i in GD.Range(unitQuantities[unit]))
			{
				addUnitInstance(unit);
			}
		}
		GD.Print(unitInstances.Count);
		foreach(int i in GD.Range(unitInstances.Count))
		{
			GD.Print(unitInstances[i]);
		}
	}

	public void addUnitInstance(Unit unit)
	{
		var unitScene = GD.Load<PackedScene>("res://Scenes/Unit.tscn");
		var unitNode = unitScene.Instantiate<Node2D>();
		unitNode.Name = unit.name;
		UnitInstance unitInstance = new UnitInstance(unit, unitNode);
		unitInstances.Add(unitInstance);
		int xPos = (isEnemy ? 2000 : 1000) + unitInstances.Count % 5 * 100;
		int yPos = unitInstances.Count/5 * 100 + 500;
		unitNode.Position = new Vector2(xPos, yPos);
		game.currentNode.AddChild(unitNode);        
		unitQuantities[unit] += 1;
	}

	public void removeUnitInstance(Unit unit)
	{
		unitQuantities[unit] -= 1;
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (var pair in unitQuantities)
		{
			stringBuilder.Append(pair.Key + ": " + pair.Value + "\n");
		}
		return stringBuilder.ToString();
	}
}
