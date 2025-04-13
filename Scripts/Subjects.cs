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

	public const int INIT_COL_SIZE = 5;
	public const int INIT_SPACING = 30;

	public Subjects(List<Unit> units, bool isEnemy)
	{
		this.isEnemy = isEnemy;
		unitTypes = units;
		unitInstances = units.ToDictionary(unit => unit, unit => new List<UnitInstance>());
		foreach (var unit in unitTypes)
		{
			foreach(int i in GD.Range(unit.baseQuantity))
			{
				UnitInstance unitInstance = new UnitInstance(unit, null);
				unitInstances[unit].Add(unitInstance);
			}
		}
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
		List<Unit> toInstantiate = new List<Unit>();
		foreach (Unit unit in unitTypes)
		{
			int remainingCount = instUnitCounts[unit] - unitInstances[unit].Count;
			for (int i = 0; i < remainingCount; i++)
			{
				toInstantiate.Add(unit);
			}
		}

		for (int i = 0; i < toInstantiate.Count; i++)
		{
			int j = GD.RandRange(i, toInstantiate.Count - 1);
			(toInstantiate[i], toInstantiate[j]) = (toInstantiate[j], toInstantiate[i]);
		}

		List<UnitInstance> allInstances = new List<UnitInstance>();

		foreach (Unit unit in toInstantiate)
		{
			int oldCount = unitInstances[unit].Count;

			addUnitInstance(unit);

			UnitInstance newInstance = unitInstances[unit][oldCount];
			allInstances.Add(newInstance);
		}

		for (int index = 0; index < allInstances.Count; index++)
		{
			positionUnit(allInstances[index], index);
		}
	}

	public void addUnitInstance(Unit unit)
	{
		var unitScene = GD.Load<PackedScene>("res://Scenes/Unit.tscn");
		var unitNode = unitScene.Instantiate<Node2D>();
		unitNode.Name = unit.name;

		UnitInstance unitInstance = new UnitInstance(unit, unitNode);
		unitInstance.isEnemy = isEnemy;
		unitInstance.health = unit.stats["health"] * unit.statModifiers["health"] * (isEnemy ? game.enemyGlobalModifiers["health"] : game.globalModifiers["health"]);

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

	public void positionUnit(UnitInstance unitInstance, int currentCount)
	{
		int total = totalInstances();
		int dynamicColSize = (int)Mathf.Ceil(Mathf.Sqrt(total));
		int dynamicRowSize = (int)Mathf.Ceil((float)total / dynamicColSize);
		int dynamicSpacing = INIT_SPACING - (int)(Mathf.Log(total + 1) * 2);
		dynamicSpacing = Math.Max(20, dynamicSpacing);
		
		int col = currentCount % dynamicColSize;
		int row = currentCount / dynamicColSize;

		int rowOffset = row - dynamicRowSize / 2;

		int direction = isEnemy ? 1 : -1;
		int originX = isEnemy ? 600 : 300;
		int xPos = originX + col * dynamicSpacing * direction;
		int yPos = 250 + rowOffset * dynamicSpacing;

		unitInstance.correspondingNode.Position = new Vector2(xPos, yPos);

		Game.setNodeTexture(unitInstance.correspondingNode, unitInstance.unitType.name, new Vector2(30, 30));
	}

	public void newUnit(Unit unit)
	{
		unitInstances[unit] = [];
		foreach(int i in GD.Range(unit.baseQuantity))
		{
			GD.Print(i);
			UnitInstance unitInstance = new UnitInstance(unit, null);
			unitInstances[unit].Add(unitInstance);
		}
		unitTypes.Add(unit);
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
