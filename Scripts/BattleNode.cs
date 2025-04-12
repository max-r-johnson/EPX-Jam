using Godot;
using System;

public partial class BattleNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public override void _Ready()
	{
		GameManager.Game.currentNode = this;
		subjects.instantiateUnits();
		enemySubjects.instantiateUnits();
		// game.currentNode = this;
		subjects.incLives(10);
		GD.Print(subjects.ToString());
		subjects.decLives(18);
		GD.Print(subjects.ToString());
	}
}
