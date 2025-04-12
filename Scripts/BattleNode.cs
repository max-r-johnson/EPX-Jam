using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Shop shop {get {return game.shop;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public override async void _Ready()
	{
		game.currentNode = this;
		game.subjects = new Subjects([new Murderer(), new Looter()], false);
		game.enemySubjects = new Subjects([new Murderer()], true);
		GD.Print(subjects.ToString());
		GD.Print(enemySubjects.ToString());
		setupButtons();
		await battle();
		// UnitInstance unitInstance = subjects.unitInstances[subjects.unitTypes[0]][0];
		// UnitInstance enemyInstance = enemySubjects.unitInstances[enemySubjects.unitTypes[0]][0];
		// moveTasks.Add(unitInstance.moveToEnemy(unitInstance.findTarget()));
		// moveTasks.Add(enemyInstance.moveToEnemy(enemyInstance.findTarget()));
		GD.Print("BATTLE OVER");
	}

	public async Task battle()
	{
		Dictionary<Unit, int> currentInstanceCounts = new Dictionary<Unit, int>();
		foreach (var pair in subjects.unitInstances)
		{
			currentInstanceCounts[pair.Key] = pair.Value.Count;
		}
		Dictionary<Unit, int> currentEnemyCounts = new Dictionary<Unit, int>();
		foreach (var pair in enemySubjects.unitInstances)
		{
			currentEnemyCounts[pair.Key] = pair.Value.Count;
		}
		List<Task> moveTasks = new();
		foreach (Unit unitType in subjects.unitTypes)
		{
			foreach (UnitInstance unitInstance in subjects.unitInstances[unitType])
			{
				moveTasks.Add(unitInstance.moveToEnemy(unitInstance.findTarget()));
			}
		}
		foreach (Unit unitType in enemySubjects.unitTypes)
		{
			foreach(UnitInstance unitInstance in enemySubjects.unitInstances[unitType])
			{
				moveTasks.Add(unitInstance.moveToEnemy(unitInstance.findTarget()));
			}
		}
		await Task.WhenAll(moveTasks);
		subjects.instantiateUnits(currentInstanceCounts);
		enemySubjects.instantiateUnits(currentEnemyCounts);
	}

	public void setupButtons()
	{
		Button refresh = GetNode<Button>("Refresh");
		refresh.Pressed += OnRefresh;

		Button closeShop = GetNode<Button>("Close Shop");
		closeShop.Pressed += OnCloseShop;

		Button endTurn = GetNode<Button>("End Turn");
		endTurn.Pressed += OnEndTurn;
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
		subjects.incLives(10);
		GD.Print(subjects.ToString());
	}
}
