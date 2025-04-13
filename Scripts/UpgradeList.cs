using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class MurdererUpgrade : Upgrade
{
	public MurdererUpgrade() : base()
	{
		text = "Upgrade the base stats of your murderers.";
	}

	public override void upgradeMethod()
	{
		applyBuff("Murderer").stats["attack"] += 1;
		applyBuff("Murderer").stats["health"] += 3;
		applyBuff("Murderer").statModifiers["attack speed"] *= 1.025f;
		applyBuff("Murderer").statModifiers["movement speed"] *= 1.025f;
	}
}

public partial class LootersFirst : Upgrade
{
	public LootersFirst() : base()
	{
		text = "Add a fast mob of looters to your sinners.";
	}
	public override void upgradeMethod()
	{
		Unit looter = new Looter();
		shop.availableUpgrades.Add(new LooterUpgrade());
		shop.availableUpgrades.Remove(this);
		game.subjects.newUnit(looter);
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
		applyBuff("Looter").stats["attack"] += 0.5f;
		applyBuff("Looter").stats["health"] += 2;
		applyBuff("Looter").statModifiers["attack speed"] *= 1.025f;
		applyBuff("Looter").statModifiers["movement speed"] *= 1.025f;
	}
}

public partial class DemonsFirst : Upgrade
{
	public DemonsFirst() : base()
	{
		text = "Add a fast mob of looters to your sinners.";
	}
	public override void upgradeMethod()
	{
		Unit demon = new Demon();
		shop.availableUpgrades.Add(new DemonUpgrade());
		shop.availableUpgrades.Remove(this);
		game.subjects.newUnit(demon);
	}
}

public partial class DemonUpgrade : Upgrade
{
	public DemonUpgrade() : base()
	{
		text = "Upgrade the base stats of your demons.";
	}
	public override void upgradeMethod()
	{
		applyBuff("Demon").stats["attack"] += 3;
		applyBuff("Demon").stats["health"] += 10;
		applyBuff("Demon").statModifiers["attack speed"] *= 1.025f;
		applyBuff("Demon").statModifiers["movement speed"] *= 1.025f;
	}
}

public partial class Pride : Upgrade
{
	public Pride() : base()
	{
		text = "Units grow stronger the fewer there are.";
	}
	public override void upgradeMethod()
	{
		foreach(Unit unitType in game.subjects.unitTypes)
		{
			int unitTotal = game.subjects.unitInstances[unitType].Count;
			game.globalModifiers["attack"] *= (float)Math.Min(Math.Pow(0.985, unitTotal) + 0.5, 1.2f);
			game.globalModifiers["health"] *= (float)Math.Min(Math.Pow(0.985, unitTotal) + 0.5, 1.2f);
		}
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
		game.globalModifiers["attack"] *= 1.1f;
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
		game.enemyGlobalModifiers["attack speed"] *= 0.9f;
		game.enemyGlobalModifiers["movement speed"] *= 0.9f;
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
	}
}

public partial class Greed : Upgrade
{
	public Greed() : base()
	{
		text = "Gain double the cost of this action as lives next turn.";
	}
	public override void upgradeMethod()
	{
		shop.greedDividend = cost * 2;
		shop.greedCount += 1;
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
				unitType.stats["attack"] += 0.5f;
				unitType.stats["health"] += 2;
				unitType.statModifiers["attack speed"] *= 1.025f;
				unitType.statModifiers["movement speed"] *= 1.025f;
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
		game.globalModifiers["health"] *= 1.1f;
	}
}

public partial class AnubisScale : Upgrade
{
	public AnubisScale() : base()
	{
		title = "Anubis's Scale";
		text = "Balance the distribution of your units slightly.";
	}

	public override void upgradeMethod()
	{
		Unit largestForce = game.subjects.unitTypes[0];
		Unit smallestForce = game.subjects.unitTypes[0];

		foreach(Unit unitType in game.subjects.unitTypes)
		{
			if(game.subjects.unitInstances[unitType].Count > game.subjects.unitInstances[largestForce].Count)
			{
				largestForce = unitType;
			}
			if(game.subjects.unitInstances[unitType].Count < game.subjects.unitInstances[smallestForce].Count)
			{
				smallestForce = unitType;
			}
		}
		foreach(int i in GD.Range(3))
		{
			UnitInstance smallestUnitInstance = new UnitInstance(smallestForce, null);
			game.subjects.unitInstances[smallestForce].Add(smallestUnitInstance);
			game.subjects.unitInstances[largestForce].RemoveAt(game.subjects.unitInstances[largestForce].Count - 1);
		}
	}
}
