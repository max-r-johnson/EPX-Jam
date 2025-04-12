using Godot;
using System;

public partial class Murderer : Unit
{
    public Murderer() : base() 
    {
        attack = 3;
        baseQuantity = 4;
    }

    public override void upgrade()
    {
        attack += 3;
        quantity += 4;
    }
}