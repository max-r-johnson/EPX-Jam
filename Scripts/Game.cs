using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

public partial class Game
{
	public Shop shop {get; set;}
	public Subjects subjects {get; set;}
	public Subjects enemySubjects {get; set;}
	public Node currentNode {get; set;}
	public int currentRound {get; set;}
	public Game()
	{
		shop = new Shop();
		subjects = new Subjects([new Murderer(), new Looter()], false);
		enemySubjects = new Subjects([new Murderer()], true);
	}

	public void nextRound()
	{
		currentRound += 1;
		enemySubjects = new Subjects([new Murderer(10)], true);
		// enemySubjects.instantiateUnits();
	}

	public static void setNodeTexture(Node node, string text, Vector2 desiredSize)
	{
		Sprite2D sprite = node.GetNode<Sprite2D>("Sprite2D");
		sprite.Texture = (Texture2D)GD.Load("res://Sprites/" + text + ".png");
		var textureSize = sprite.Texture.GetSize();
		sprite.Scale = desiredSize / textureSize;
	}
}
