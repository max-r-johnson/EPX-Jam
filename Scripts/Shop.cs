using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public partial class Shop
{
    public HashSet<Upgrade> availableUpgrades { get; set; }
    public List<Upgrade> currentUpgrades { get; set; }
    public int rollAmount { get; set; }
    public Game game {get {return GameManager.Game;}}
    public int shopSize { get; set; }
    public int dupLimit { get; set; }
    public Shop()
    {
        rollAmount = 1;
        shopSize = 6;
        dupLimit = 2;
    }

    public void rollShop()
    {
        game.subjects.decLives(rollAmount);
        Random random = new Random();
        int remainder = shopSize;
        for (int i = remainder; i > 0; i--)
        {
            currentUpgrades.Add(availableUpgrades.ElementAt(random.Next(availableUpgrades.Count)));
            if currentUpgrades[-1].Count() > dupLimit:
            {
                currentUpgrades.RemoveAt(-1);
                remainder++;
            }
        }
        //Display currentUpgrades
    }

    //Roll shop after each round and at start of game, or after reroll
    //For each roll, take upgrades from availableUpgrades, add them to currentUpgrades, and display them
    //Randomly display 6 upgrades, possibly reduce to 4, duplicates allowed but no more than 2
    //Upgrade is an object
}