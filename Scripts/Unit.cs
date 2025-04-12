using Godot;
using System;

public partial class Unit
{
    public int attack { get; set; }
    public int speed { get; set; }
    public virtual int baseQuantity => 1;
    public int quantity { get; set; }
	public string name { get; set; }
	public Unit()
	{
		attack = 1;
		speed = 1;
		quantity = baseQuantity;
	}

	public virtual void upgrade()
	{

	}
}
