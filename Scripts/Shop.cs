using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Shop
{
    public HashSet<Upgrade> availableUpgrades { get; set; }
    public List<Upgrade> currentUpgrades { get; set; }
    public int rollAmount { get; set; }
    public Game game {get {return GameManager.Game;}}
    public Shop()
    {
        rollAmount = 1;
    }

    public void rollShop()
    {
        game.subjects.decLives(rollAmount);
    }
}