using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Shop
{
    public Hashset<Upgrade> availableUpgrades { get; set; }
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

    //Roll shop after each round and at start of game, or after reroll
    //For each roll, take upgrades from availableUpgrades, add them to currentUpgrades, and display them
    //Randomly display 6 upgrades, possibly reduce to 4, duplicates allowed but no more than 2
    //Upgrade is an object
   
}