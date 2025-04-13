using Godot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Greed greed { get; set; } = new Greed();
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
		if (greed.greedFlag == true)
		{
			subjects.incLives(greed.dividend);
			greed.greedFlag = false;
		}
		scaleEnemies();

		shop.refreshShop();
		GetTree().ChangeSceneToFile("res://Scenes/Shop.tscn");
	}

	public void scaleEnemies()
	{
		GD.Print((int)Math.Floor(Shop.INIT_ROUND_LIVES * Math.Pow(1.1, game.currentRound)));
		enemySubjects.incLives((int)Math.Floor(Shop.INIT_ROUND_LIVES * Math.Pow(1.05, game.currentRound)));
		foreach(Unit unitType in enemySubjects.unitTypes)
		{
			unitType.stats["health"] *= 1.05f;
			unitType.stats["attack"] *= 1.05f;
		}
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
