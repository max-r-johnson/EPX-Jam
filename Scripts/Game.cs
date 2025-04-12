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
        subjects = new Subjects([new Murderer(), new Looter()]);
        subjects.isEnemy = false;
        enemySubjects = new Subjects([new Murderer()]);
        enemySubjects.isEnemy = true;
    }

    public void nextRound()
    {
        currentRound += 1;
        enemySubjects = new Subjects([new Murderer(10)]);
        enemySubjects.instantiateUnits();
    }

    public static void setNodeTexture(Node2D node, string text)
    {
        Sprite2D sprite = (Sprite2D)node.GetChildren()[0];
        sprite.Texture = (Texture2D)GD.Load("res://Sprites/" + text + ".png");
        var desiredSize = new Vector2(96, 96);
        var textureSize = sprite.Texture.GetSize();
        sprite.Scale = desiredSize / textureSize;
    }
}