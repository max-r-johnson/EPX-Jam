using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Subjects
{
	public List<Unit> unitTypes { get; set; }
	public Game game { get { return GameManager.Game; } }
	public Dictionary<Unit, List<UnitInstance>> unitInstances { get; set; }
	public bool isEnemy { get; set; }

	public const int ROW_SIZE = 5;
	public const int SPACING = 100;

	public Subjects(List<Unit> units, bool isEnemy)
	{
		this.isEnemy = isEnemy;
		unitTypes = units;
		unitInstances = units.ToDictionary(unit => unit, unit => new List<UnitInstance>());
		Dictionary<Unit, int> instUnitCounts = new Dictionary<Unit, int>();
		foreach (var unit in unitTypes)
		{
			instUnitCounts[unit] = unit.baseQuantity;
		}
		instantiateUnits(instUnitCounts);
	}

	public void incLives(int amount)
	{
		var currentWeights = unitInstances.ToDictionary(pair => pair.Key, pair => pair.Value.Count);
		int total = currentWeights.Values.Sum();
		if (total == 0) return;

		foreach (int i in GD.Range(amount))
		{
			int randomValue = GD.RandRange(0, total - 1);
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
		reinstantiate();
	}

	public void decLives(int amount)
	{
		foreach (int i in GD.Range(amount))
		{
			int total = totalInstances();
			if (total == 0) break;

			int randomValue = GD.RandRange(0, total - 1);
			int cumulative = 0;

			foreach (var pair in unitInstances)
			{
				int count = pair.Value.Count;
				cumulative += count;
				if (randomValue < cumulative && count > 0)
				{
					removeUnitInstance(pair.Key);
					break;
				}
			}
		}
		reinstantiate();
	}

	public void reinstantiate()
	{
		Dictionary<Unit, int> currentCounts = new Dictionary<Unit, int>();
		foreach (var pair in unitInstances)
		{
			currentCounts[pair.Key] = pair.Value.Count;
		}

		foreach (var pair in unitInstances)
		{
			foreach (UnitInstance instance in pair.Value)
			{
				instance.correspondingNode.QueueFree();
			}
			pair.Value.Clear();
		}

		instantiateUnits(currentCounts);
	}

	public void instantiateUnits(Dictionary<Unit, int> instUnitCounts)
	{
		int totalToAdd = 0;
		Dictionary<Unit, int> remaining = new Dictionary<Unit, int>();

		foreach (Unit unit in unitTypes)
		{
			int remainingCount = instUnitCounts[unit] - unitInstances[unit].Count;
			if (remainingCount > 0)
			{
				remaining[unit] = remainingCount;
				totalToAdd += remainingCount;
			}
		}

		foreach (int i in GD.Range(totalToAdd))
		{
			if (remaining.Count == 0) break;

			int randomValue = GD.RandRange(0, totalToAdd - 1);
			int cumulative = 0;

			foreach (var pair in remaining)
			{
				cumulative += pair.Value;
				if (randomValue < cumulative)
				{
					addUnitInstance(pair.Key);
					remaining[pair.Key] -= 1;

					if (remaining[pair.Key] <= 0)
					{
						remaining.Remove(pair.Key);
					}

					totalToAdd -= 1;
					break;
				}
			}
		}
	}

	public void addUnitInstance(Unit unit)
	{
		var unitScene = GD.Load<PackedScene>("res://Scenes/Unit.tscn");
		var unitNode = unitScene.Instantiate<Node2D>();
		unitNode.Name = unit.name;

		UnitInstance unitInstance = new UnitInstance(unit, unitNode);
		unitInstance.isEnemy = isEnemy;
		int count = totalInstances();
		int xPos = (isEnemy ? 2000 : 1000) + count / ROW_SIZE * -SPACING;
		int yPos = count % ROW_SIZE * SPACING + 500;
		unitNode.Position = new Vector2(xPos, yPos);

		Game.setNodeTexture(unitNode, unit.name);
		unitInstances[unit].Add(unitInstance);
		game.currentNode.AddChild(unitNode);
	}

	public void removeUnitInstance(Unit unit)
	{
		if (unitInstances[unit].Count == 0) return;

		var removedInstance = unitInstances[unit].Last();
		unitInstances[unit].RemoveAt(unitInstances[unit].Count - 1);
		removedInstance.correspondingNode.QueueFree();
	}

	public int totalInstances()
	{
		return unitInstances.Values.Sum(list => list.Count);
	}

	public override string ToString()
	{
		StringBuilder stringBuilder = new StringBuilder();
		foreach (var pair in unitInstances)
		{
			stringBuilder.AppendLine($"{pair.Key}: {pair.Value.Count}");
		}
		return stringBuilder.ToString();
	}
}
