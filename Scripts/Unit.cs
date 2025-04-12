using Godot;
using System;

public partial class Unit
{
	public int attack;
	public int speed;
	public int baseQuantity;
	public int quantity;
	public Unit()
	{
		attack = 1;
		speed = 1;
		baseQuantity = 1;
		quantity = baseQuantity;
	}

	public virtual void upgrade()
	{

	}
}
