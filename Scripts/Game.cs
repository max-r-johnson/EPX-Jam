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
}