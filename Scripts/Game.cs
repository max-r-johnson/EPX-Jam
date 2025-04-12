using Godot;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

public partial class Game
{
    public Shop shop {get; set;}
    public Subjects subjects {get; set;}
    public Game()
    {
        shop = new Shop();
        subjects = new Subjects([new Murderer(), new Looter()]);
        subjects.incLives(10);
        GD.Print(subjects.ToString());
        subjects.decLives(18);
        GD.Print(subjects.ToString());
    }
}