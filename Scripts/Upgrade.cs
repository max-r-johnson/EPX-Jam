using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class Upgrade
{
    public int cost { get; set; }
    public float costMultiplier { get; set; } = 1;
    public string text { get; set; }
    public Game game { get { return GameManager.Game; }}
    public Shop shop { get { return game.shop; }}
    public string title { get; set; }
    public Upgrade()
    {
        cost = 3;
    }
    
    public virtual void upgradeMethod()
    {
        
    }

    public virtual Unit applyBuff(string text)
    {
        foreach (var unitType in game.subjects.unitTypes)
        {
            if (unitType.GetType().Name == text)
            {
                return unitType;
            }
        }
        return null;
    }
}