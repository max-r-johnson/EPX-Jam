using Godot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Shop shop {get {return game.shop;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}

	public override async void _Ready()
	{
		game.currentNode = this;
		Dictionary<Unit, int> currentCounts = new Dictionary<Unit, int>();
		foreach (var pair in subjects.unitInstances)
		{
			currentCounts[pair.Key] = pair.Value.Count;
		}
		foreach(Unit unitType in game.subjects.unitTypes)
		{
			game.subjects.unitInstances[unitType] = [];
		}
		game.subjects.instantiateUnits(currentCounts);
		foreach (var pair in enemySubjects.unitInstances)
		{
			currentCounts[pair.Key] = pair.Value.Count;
		}
		foreach(Unit unitType in game.enemySubjects.unitTypes)
		{
			game.enemySubjects.unitInstances[unitType] = [];
		}
		game.enemySubjects.instantiateUnits(currentCounts);
		GD.Print(subjects.ToString());
		GD.Print(enemySubjects.ToString());
		shop.closeShop();
		await Task.Delay(2000);
		await battle();
	}

	public async Task battle()
	{
		Dictionary<Unit, int> currentInstanceCounts = new();
		foreach (var pair in subjects.unitInstances)
			currentInstanceCounts[pair.Key] = pair.Value.Count;

		Dictionary<Unit, int> currentEnemyCounts = new();
		foreach (var pair in enemySubjects.unitInstances)
			currentEnemyCounts[pair.Key] = pair.Value.Count;

		CancellationTokenSource cts = new();
		List<Task> moveTasks = new();

		foreach (Unit unitType in subjects.unitTypes)
		{
			foreach (UnitInstance unitInstance in subjects.unitInstances[unitType])
			{
				moveTasks.Add(ExecuteMoveTask(unitInstance, cts.Token));
			}
		}

		foreach (Unit unitType in enemySubjects.unitTypes)
		{
			foreach (UnitInstance unitInstance in enemySubjects.unitInstances[unitType])
			{
				moveTasks.Add(ExecuteMoveTask(unitInstance, cts.Token));
			}
		}

		_ = Task.Run(async () =>
		{
			while (true)
			{
				if (AllUnitsDead())
				{
					cts.Cancel();
					break;
				}
				await Task.Delay(500);
			}
		});

		await Task.WhenAll(moveTasks);
		subjects.instantiateUnits(currentInstanceCounts);
		enemySubjects.instantiateUnits(currentEnemyCounts);
		startTurn();
	}

	public void startTurn()
	{
		game.currentRound += 1;
		subjects.incLives(shop.roundLives);
		subjects.incLives(shop.greedCount * shop.greedDividend);
		shop.greedCount = 0;
		scaleEnemies();

		shop.refreshShop();
		GetTree().ChangeSceneToFile("res://Scenes/Shop.tscn");
	}

	public void scaleEnemies()
	{
		game.enemyGlobalModifiers["health"] *= 1.025f;
		game.enemyGlobalModifiers["attack"] *= 1.025f;
		if(game.currentRound==2)
		{
			enemySubjects.newUnit(new Looter());
			Dictionary<Unit, int> currentCounts = new Dictionary<Unit, int>();
			foreach (var pair in game.enemySubjects.unitInstances)
			{
				currentCounts[pair.Key] = pair.Value.Count;
			}
			foreach(Unit unitType in game.enemySubjects.unitTypes)
			{
				game.enemySubjects.unitInstances[unitType] = [];
			}
			enemySubjects.instantiateUnits(currentCounts);
			return;
		}
		else if(game.currentRound==5)
		{
			enemySubjects.newUnit(new Demon());
			Dictionary<Unit, int> currentCounts = new Dictionary<Unit, int>();
			foreach (var pair in game.enemySubjects.unitInstances)
			{
				currentCounts[pair.Key] = pair.Value.Count;
			}
			foreach(Unit unitType in game.enemySubjects.unitTypes)
			{
				game.enemySubjects.unitInstances[unitType] = [];
			}
			enemySubjects.instantiateUnits(currentCounts);
		}
		else if(game.currentRound==9)
		{
			enemySubjects.newUnit(new Lucifer());
			Dictionary<Unit, int> currentCounts = new Dictionary<Unit, int>();
			foreach (var pair in game.enemySubjects.unitInstances)
			{
				currentCounts[pair.Key] = pair.Value.Count;
			}
			foreach(Unit unitType in game.enemySubjects.unitTypes)
			{
				game.enemySubjects.unitInstances[unitType] = [];
			}
			enemySubjects.instantiateUnits(currentCounts);
			return;
		}
		enemySubjects.incLives((int)Math.Floor(Shop.INIT_ROUND_LIVES * Math.Pow(1.01, game.currentRound)));
	}

	private async Task ExecuteMoveTask(UnitInstance unitInstance, CancellationToken token)
	{
		UnitInstance target = unitInstance.findTarget();

		if (target != null)
		{
			await unitInstance.moveToEnemy(target, token);
		}
	}

	private bool AllUnitsDead()
	{
		return subjects.unitInstances.All(kvp => kvp.Value.All(u => u.health <= 0)) ||
			enemySubjects.unitInstances.All(kvp => kvp.Value.All(u => u.health <= 0));
	}
}
