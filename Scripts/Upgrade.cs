using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Upgrade
{
    public int cost { get; set; }
    public float costMultiplier { get; set; } = 1;
    public Upgrade ()
    {
        cost = 1;
    }
}