using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

public partial class Shop
{
	public HashSet<Upgrade> availableUpgrades { get; set; }
	public List<Upgrade> currentUpgrades { get; set; } = [];
	public Game game {get {return GameManager.Game;}}
	public int spentLives { get; set; }
	public const int ROLL_AMOUNT = 1;
	public const int SHOP_SIZE = 6;
	public const int DUPE_LIMIT = 2;
	public Shop()
	{
		availableUpgrades = new HashSet<Upgrade>
		{
			new MurdererUpgrade(),
			new LooterUpgrade(),
			new Pride(),
			new Wrath(),
			new Sloth(),
			new Lust(),
			new Greed(),
			new Envy(),
			new Gluttony(),
		};
	}

	public void refreshShop()
	{
		currentUpgrades = [];
		while (currentUpgrades.Count < SHOP_SIZE && availableUpgrades.Count > 0)
		{
			int index = GD.RandRange(0, availableUpgrades.Count - 1);
			Upgrade selected = availableUpgrades.ElementAt(index);

			int count = currentUpgrades.Count(u => u == selected);
			if (count < DUPE_LIMIT)
			{
				currentUpgrades.Add(selected);
			}
		}
		foreach (Upgrade upgrade in currentUpgrades)
		{
			GD.Print(upgrade.GetType());
		}
		GD.Print("\n");
	}

	public void closeShop()
	{
		game.subjects.decLives(spentLives);
		spentLives = 0;
		GD.Print(game.subjects.ToString());
	}
}
