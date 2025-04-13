using Godot;
using System;
using System.Collections.Generic;

public partial class Unit
{
	public Dictionary<string,float> stats { get; set; }
	public virtual int baseQuantity { get; set; } = 1;
	public string name { get; set; }
	public Unit()
	{
		stats = stats = new Dictionary<string, float>
        {
            { "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1},
        };
	}

    public Unit(int quantity, Dictionary<string, float> stats = null)
    {
        baseQuantity = quantity;

        this.stats = stats ?? new Dictionary<string, float>
        {
            { "attack", 1 },
			{ "health", 1 },
			{ "attack speed", 1 },
			{ "movement speed", 1 },
        };
    }

	public virtual void upgrade()
	{

	}
}
