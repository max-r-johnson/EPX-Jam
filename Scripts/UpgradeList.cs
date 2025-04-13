using Godot;
using System;

public partial class MurdererUpgrade : Upgrade
{
    public MurdererUpgrade() : base()
    {
        text = "Upgrade the base stats of your murderers.";
    }

    public override void upgradeMethod()
    {

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
            unitType.stats["attack"] *= 1.25f;
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
        
    }
}

public partial class Gluttony : Upgrade
{
    public Gluttony() : base()
    {
        text = "";
    }
    public override void upgradeMethod()
    {
        
    }
}