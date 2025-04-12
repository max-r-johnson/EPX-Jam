using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Shop
{
    public List<Upgrade> availableUpgrades { get; set; }
    public List<Upgrade> currentUpgrades { get; set; }
    public int rollAmount { get; set; }
    public Shop()
    {
        rollAmount = 1;
    }

    public void rollShop()
    {
        GameManager.Game.subjects.decLives(rollAmount);
    }
   
}