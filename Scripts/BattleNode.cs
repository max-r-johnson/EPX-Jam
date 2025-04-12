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
		List<Task> moveTasks = new();
		foreach(UnitInstance unitInstance in game.subjects.unitInstances[game.subjects.unitTypes[0]])
		{
			moveTasks.Add(unitInstance.moveToEnemy(unitInstance.findTarget()));
		}
		foreach(UnitInstance unitInstance in game.enemySubjects.unitInstances[game.enemySubjects.unitTypes[0]])
		{
			moveTasks.Add(unitInstance.moveToEnemy(unitInstance.findTarget()));
		}
		await Task.WhenAll(moveTasks);
		GD.Print(subjects.ToString());
		GD.Print(enemySubjects.ToString());
		setupButtons();
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
