using Godot;
using System;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Subjects subjects {get; set;}
	public Subjects enemySubjects {get; set;}
	public int spentLives {get; set;} = 0;
	public override void _Ready()
	{
		game.currentNode = this;
		subjects = new Subjects([new Murderer(), new Looter()], false);
		enemySubjects = new Subjects([new Murderer()], true);
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
		spentLives += 1;
	}

	private void OnCloseShop()
	{
		subjects.decLives(spentLives);
		spentLives = 0;
		GD.Print(subjects.ToString());
	}

	private void OnEndTurn()
	{
		subjects.incLives(10);
		GD.Print(subjects.ToString());
	}
}
