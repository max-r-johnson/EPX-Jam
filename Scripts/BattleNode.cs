using Godot;
using System;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public override void _Ready()
	{
		game.currentNode = this;
		subjects.instantiateUnits();
		enemySubjects.instantiateUnits();
		GD.Print(subjects.ToString());
		GD.Print(enemySubjects.ToString());
		setupButtons();
	}

	public void setupButtons()
	{
		Button refresh = GetNode<Button>("Refresh");
		refresh.Pressed += OnRefresh;

		Button endTurn = GetNode<Button>("End Turn");
		endTurn.Pressed += OnEndTurn;
	}

	private void OnRefresh()
	{
		subjects.decLives(1);
		GD.Print(subjects.ToString());
	}

	private void OnEndTurn()
	{
		subjects.incLives(10);
		GD.Print(subjects.ToString());
	}
}
