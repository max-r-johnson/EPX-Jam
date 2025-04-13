using Godot;
using System;
using System.Collections.Generic;

public partial class MurdererUpgrade : Upgrade
{
    public MurdererUpgrade() : base()
    {
<<<<<<< HEAD
        cost = 1;
        game.subjects.unitTypes[0].stats[]
=======
        text = "Upgrade the base stats of your murderers.";
    }

    public override void upgradeMethod()
    {

>>>>>>> Max2
    }
}

public partial class LooterUpgrade : Upgrade
{
    public LooterUpgrade() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "Upgrade the base stats of your looters.";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}

public partial class Pride : Upgrade
{
    public Pride() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}

public partial class Wrath : Upgrade
{
    public Wrath() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "Upgrade the attack of your units by 10%.";
    }
    public override void upgradeMethod()
    {
        foreach(Unit unitType in game.subjects.unitTypes)
        {
            unitType.stats["attack"] *= 1.25f;
            GD.Print(unitType.stats["attack"]);
        }
>>>>>>> Max2
    }
}

public partial class Sloth : Upgrade
{
    public Sloth() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "Slow enemy movement and attack speed by 10%.";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}

public partial class Lust : Upgrade
{
    public Lust() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "Gain more lives at the beginning of each turn.";
    }
    public override void upgradeMethod()
    {
        shop.roundLives += 1;
		GD.Print("new round lives: " + shop.roundLives);
>>>>>>> Max2
    }
}

public partial class Greed : Upgrade
{
    public Greed() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}

public partial class Envy : Upgrade
{
    public Envy() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "If you have a unit the last enemy had, level it up.";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}

public partial class Gluttony : Upgrade
{
    public Gluttony() : base()
    {
<<<<<<< HEAD
        cost = 1;
=======
        text = "";
    }
    public override void upgradeMethod()
    {
        
>>>>>>> Max2
    }
}