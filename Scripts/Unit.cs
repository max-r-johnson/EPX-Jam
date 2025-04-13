using Godot;
using System;
using System.Collections.Generic;

public partial class Unit
{
	public Dictionary<string,float> stats { get; set; }
	public Dictionary<string,float> statModifiers { get; set;}
	public virtual int baseQuantity { get; set; } = 1;
	public string name { get; set; }
	public Unit()
	{
		stats = new Dictionary<string, float>
        {
            { "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1},
        };
		statModifiers = new Dictionary<string, float>
        {
            { "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1},
        };
	}

    public Unit(int quantity, Dictionary<string, float> statModifiers = null)
    {
        baseQuantity = quantity;

        this.statModifiers = statModifiers ?? new Dictionary<string, float>
        {
            { "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1 },
        };
    }
}
