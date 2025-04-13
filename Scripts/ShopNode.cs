using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ShopNode : Node
{
    public Game game {get {return GameManager.Game;}}
	public Shop shop {get {return game.shop;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public override async void _Ready()
	{
        game.currentNode = this;
    }
}