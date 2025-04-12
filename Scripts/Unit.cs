using Godot;
using System;
using System.Collections.Generic;

public partial class Unit
{
    public int attack { get; set; }
    public int speed { get; set; }
	public Dictionary<string,int> stats { get; set; }
	public virtual int baseQuantity { get; set; } = 1;
	public string name { get; set; }
	public Unit()
	{
		stats = stats = new Dictionary<string, int>
        {
            { "attack", 1 },
            { "speed", 1 },
			{ "health", 1 },
			{ "attack speed", 1 }
        };
	}

    public Unit(int quantity, Dictionary<string, int> stats = null)
    {
        baseQuantity = quantity;

        this.stats = stats ?? new Dictionary<string, int>
        {
            { "attack", 1 },
            { "speed", 1 },
			{ "health", 1 },
			{ "attack speed", 1 }
        };
    }

	public virtual void upgrade()
	{

	}
}
