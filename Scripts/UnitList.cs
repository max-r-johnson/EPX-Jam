using Godot;
using System;
using System.Collections.Generic;

public partial class Murderer : Unit
{
    public override int baseQuantity { get; set; } = 10;
    public Murderer() : base() 
    {        
        name = "Murderer";
        stats["attack"] = 3;
        stats["health"] = 10;
        stats["movement speed"] = 2;
    }
}

public partial class Looter : Unit
{
    public override int baseQuantity { get; set; } = 18;
    public Looter() : base() 
    {        
        name = "Looter";
        stats["attack"] = 1;
        stats["attack speed"] = 1.25f;
        stats["health"] = 8;
        stats["movement speed"] = 3;
    }
}

public partial class Demon : Unit
{
    public override int baseQuantity { get; set; } = 3;
    public Demon() : base() 
    {        
        name = "Demon";
        stats["attack"] = 10;
        stats["attack speed"] = .5f;
        stats["health"] = 30;
    }
}

public partial class Lucifer : Unit
{
    public override int baseQuantity { get; set; } = 1;
    public Lucifer() : base() 
    {        
        name = "Lucifer";
        stats["attack"] = 50;
        stats["health"] = 2000;
    }
}