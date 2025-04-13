using Godot;
using System;
using System.Collections.Generic;

public partial class Murderer : Unit
{
    public override int baseQuantity { get; set; } = 4;
    public Murderer() : base() 
    {        
        name = "Murderer";
        stats["attack"] = 3;
        stats["health"] = 10;
        stats["movement speed"] = 2;
    }

    public Murderer(int quantity, Dictionary<string, float> stats = null) : base(quantity, stats) 
    {        
        name = "Murderer";
    }

    public override void upgrade()
    {
        stats["attack"] += 3;
    }
}

public partial class Looter : Unit
{
    public override int baseQuantity { get; set; } = 6;
    public Looter() : base() 
    {        
        name = "Looter";
        stats["attack"] = 1;
        stats["attack speed"] = 1.25f;
        stats["health"] = 8;
        stats["movement speed"] = 3;
    }

    public Looter(int quantity, Dictionary<string, float> stats = null) : base(quantity, stats) 
    {        
        name = "Looter";
    }

    public override void upgrade()
    {
        stats["attack"] += 1;
    }
}