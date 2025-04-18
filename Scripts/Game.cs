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
	public Dictionary<string,float> globalModifiers { get; set; }
	public Dictionary<string,float> enemyGlobalModifiers { get; set; }

	public Game()
	{
		globalModifiers = new Dictionary<string, float>
		{
			{ "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1},
		};
		enemyGlobalModifiers = new Dictionary<string, float>
		{
			{ "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1},
		};
		shop = new Shop();
		subjects = new Subjects([new Murderer()], false);
		enemySubjects = new Subjects([new Murderer()], true);
	}

	public static void setNodeTexture(Node node, string text, Vector2 desiredSize)
	{
		Sprite2D sprite = node.GetNode<Sprite2D>("Sprite2D");
		sprite.Texture = (Texture2D)GD.Load("res://Sprites/" + text + ".png");
		var textureSize = sprite.Texture.GetSize();
		sprite.Scale = desiredSize / textureSize;
	}
}
