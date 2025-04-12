using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Upgrade
{
    public int cost { get; set; }
    public Upgrade ()
    {
        cost = 1;
    }
}