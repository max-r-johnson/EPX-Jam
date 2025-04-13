using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Upgrade
{
    public int cost { get; set; }
<<<<<<< HEAD
    public float costMultiplier { get; set; } = 1;
    public Game game {get {return GameManager.Game;}}
    public Upgrade ()
=======
    public string text { get; set; }
    public Game game { get { return GameManager.Game; }}
    public Shop shop { get { return game.shop; }}
    public Upgrade()
>>>>>>> Max2
    {
        cost = 3;
    }
    
    public virtual void upgradeMethod()
    {
        
    }
}