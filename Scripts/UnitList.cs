using Godot;
using System;
using System.Collections.Generic;

public partial class Murderer : Unit
{
    public override int baseQuantity => 4;
    public Murderer() : base() 
    {        
        name = "Murderer";
        stats["attack"] = 3;
    }

    public Murderer(int quantity, Dictionary<string, int> stats = null) : base(quantity, stats) 
    {        
        name = "Murderer";
    }

    public override void upgrade()
    {
        stats["attack"] += 3;
        quantity += 4;
    }
}

public partial class Looter : Unit
{
    public override int baseQuantity => 6;
    public Looter() : base() 
    {        
        name = "Looter";
        stats["attack"] = 1;
    }

    public Looter(int quantity, Dictionary<string, int> stats = null) : base(quantity, stats) 
    {        
        name = "Looter";
    }

    public override void upgrade()
    {
        stats["attack"] += 1;
        quantity += 6;
    }
}