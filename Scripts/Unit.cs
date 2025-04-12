using Godot;
using System;
using System.Collections.Generic;

public partial class Unit
{
    public int attack { get; set; }
    public int speed { get; set; }
	public Dictionary<string,int> stats { get; set; }
    public virtual int baseQuantity => 1;
    public int quantity { get; set; }
	public string name { get; set; }
	public Unit()
	{
		stats = stats = new Dictionary<string, int>
        {
            { "attack", 1 },
            { "speed", 1 },
			{ "health", 1 }
        };
		quantity = baseQuantity;
	}

    public Unit(int quantity, Dictionary<string, int> stats = null)
    {
        this.quantity = quantity;

        this.stats = stats ?? new Dictionary<string, int>
        {
            { "attack", 1 },
            { "speed", 1 },
			{ "health", 1 }
        };
    }

	public virtual void upgrade()
	{

	}
}
