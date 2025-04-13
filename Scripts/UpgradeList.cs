using Godot;
using System;
using System.Collections.Generic;

public partial class MurdererUpgrade : Upgrade
{
    public MurdererUpgrade() : base()
    {
        text = "Upgrade the base stats of your murderers.";
    }

    public override void upgradeMethod()
    {
        game.subjects.unitTypes[0].stats["attack"] += 1;
        game.subjects.unitTypes[0].stats["health"] += 3;
        game.subjects.unitTypes[0].stats["attack speed"] *= 1.1f;
        game.subjects.unitTypes[0].stats["movement speed"] *= 1.1f;
    }
}

public partial class LooterUpgrade : Upgrade
{
    public LooterUpgrade() : base()
    {
        text = "Upgrade the base stats of your looters.";
    }
    public override void upgradeMethod()
    {
        game.subjects.unitTypes[1].stats["attack"] += 0.5f;
        game.subjects.unitTypes[1].stats["health"] += 2;
        game.subjects.unitTypes[1].stats["attack speed"] *= 1.1f;
        game.subjects.unitTypes[1].stats["movement speed"] *= 1.1f;
    }
}

public partial class Pride : Upgrade
{
    public Pride() : base()
    {
        text = "";
    }
    public override void upgradeMethod()
    {
        
    }
}

public partial class Wrath : Upgrade
{
    public Wrath() : base()
    {
        text = "Upgrade the attack of your units by 10%.";
    }
    public override void upgradeMethod()
    {
        foreach(Unit unitType in game.subjects.unitTypes)
        {
            unitType.stats["attack"] *= 1.1f;
            GD.Print(unitType.stats["attack"]);
        }
    }
}

public partial class Sloth : Upgrade
{
    public Sloth() : base()
    {
        text = "Slow enemy movement and attack speed by 10%.";
    }
    public override void upgradeMethod()
    {
        foreach(Unit unitType in game.enemySubjects.unitTypes)
        {
            unitType.stats["attack speed"] *= 0.9f;
            GD.Print(unitType.stats["attack speed"]);
            unitType.stats["movement speed"] *= 0.9f;
            GD.Print(unitType.stats["movement speed"]);
        }
    }
}

public partial class Lust : Upgrade
{
    public Lust() : base()
    {
        text = "Gain more lives at the beginning of each turn.";
    }
    public override void upgradeMethod()
    {
        shop.roundLives += 1;
		GD.Print("new round lives: " + shop.roundLives);
    }
}

public partial class Greed : Upgrade
{
    public Greed() : base()
    {
        text = "";
    }
    public override void upgradeMethod()
    {
        
    }
}

public partial class Envy : Upgrade
{
    public Envy() : base()
    {
        text = "If you have a unit the last enemy had, level it up.";
    }
    public override void upgradeMethod()
    {
        foreach(Unit unitType in game.enemySubjects.unitTypes)
        {
            if (game.subjects.unitTypes.Contains(unitType))
            {
                if (unitType is Murderer)
                {
                    game.subjects.unitTypes[1].stats["attack"] += 0.5f;
                    game.subjects.unitTypes[1].stats["health"] += 2;
                    game.subjects.unitTypes[1].stats["attack speed"] *= 1.1f;
                    game.subjects.unitTypes[1].stats["movement speed"] *= 1.1f;
                }
                else if (unitType is Looter)
                {
                    game.subjects.unitTypes[1].stats["attack"] += 0.5f;
                    game.subjects.unitTypes[1].stats["health"] += 2;
                    game.subjects.unitTypes[1].stats["attack speed"] *= 1.1f;
                    game.subjects.unitTypes[1].stats["movement speed"] *= 1.1f;
                }
            }
        }
    }
}

public partial class Gluttony : Upgrade
{
    public Gluttony() : base()
    {
        text = "Upgrade the health of your units by 10%.";
    }
    public override void upgradeMethod()
    {
        foreach(Unit unitType in game.subjects.unitTypes)
        {
            unitType.stats["health"] *= 1.1f;
            GD.Print(unitType.stats["health"]);
        }
    }
}