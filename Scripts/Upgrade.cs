using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Upgrade
{
    public int cost { get; set; }
    public string text { get; set; }
    public Game game { get { return GameManager.Game; }}
    public Shop shop { get { return game.shop; }}
    public Upgrade()
    {
        cost = 3;
    }
    
    public virtual void upgradeMethod()
    {
        
    }
}