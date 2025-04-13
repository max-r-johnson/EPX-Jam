using Godot;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Greed greed { get; set; }
	public Shop shop {get {return game.shop;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public override void _Ready()
	{
		game.currentNode = this;
		game.subjects = new Subjects([new Murderer(), new Looter()], false);
		game.enemySubjects = new Subjects([new Murderer()], true);
		GD.Print(subjects.ToString());
		GD.Print(enemySubjects.ToString());
		setupButtons();
		shop.closeShop();
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
		subjects.incLives(shop.roundLives);
		if (greed.greedFlag == true)
		{
			subjects.incLives(greed.dividend);
			greed.greedFlag = false;
		}
		enemySubjects.incLives(Shop.INIT_ROUND_LIVES);
		shop.refreshShop();
		GetTree().ChangeSceneToFile("res://Scenes/Shop.tscn");
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

	public void setupButtons()
	{
		Button endTurn = GetNode<Button>("End Turn");
		endTurn.Pressed += OnEndTurn;

		// Temp button
		Button lust = GetNode<Button>("Button");
		lust.Pressed += OnLust;
	}

	private void OnLust()
	{
		new Lust().upgradeMethod();
		new Wrath().upgradeMethod();
	}

	private void OnRefresh()
	{
		shop.spentLives += Shop.ROLL_AMOUNT;
		shop.refreshShop();
	}

	private void OnCloseShop()
	{
		shop.closeShop();
	}

	private void OnEndTurn()
	{
		GD.Print(subjects.ToString());
		// Include this button in the shop scene, instead have it navigate back to battle scene
		_ = OnEndTurnAsync();
	}

	private async Task OnEndTurnAsync()
	{
		await battle();
	}
}
