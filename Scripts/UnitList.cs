using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Murderer : Unit
{
    public override int baseQuantity => 4;
    public Murderer() : base() 
    {        
        name = "Murderer";
        attack = 3;
    }

    public override void upgrade()
    {
        attack += 3;
        quantity += 4;
    }
}

public partial class Looter : Unit
{
    public override int baseQuantity => 20;
    public Looter() : base() 
    {        
        name = "Looter";
        attack = 1;
    }

    public override void upgrade()
    {
        attack += 1;
        quantity += 6;
    }
}