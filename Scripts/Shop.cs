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
	public int roundLives { get; set; } = INIT_ROUND_LIVES;
	public int greedCount { get; set; }
	public int greedDividend { get; set; } = 0;
	public const int ROLL_AMOUNT = 1;
	public const int SHOP_SIZE = 5;
	public const int DUPE_LIMIT = 1;
	public const int UNIT_DUPE_LIMIT = 0;
	public const int INIT_ROUND_LIVES = 10;
	public Shop()
	{
		availableUpgrades = new HashSet<Upgrade>
		{
			new MurdererUpgrade(),
			new LootersFirst(),
			new DemonsFirst(),
			new Pride(),
			new Wrath(),
			new Sloth(),
			new Lust(),
			new Greed(),
			new Envy(),
			new Gluttony(),
			new AnubisScale(),
		};
	}

	public void refreshShop()
	{
		currentUpgrades = [];
		while (currentUpgrades.Count < SHOP_SIZE && availableUpgrades.Count > 0)
		{
			int index = GD.RandRange(0, availableUpgrades.Count - 1);
			Upgrade selected = availableUpgrades.ElementAt(index);

			string typeName = selected.GetType().Name;
			bool isFirstType = typeName.EndsWith("First");

			int count = currentUpgrades.Count(u => u == selected);
			int maxAllowed = isFirstType ? UNIT_DUPE_LIMIT : DUPE_LIMIT;

			if (count <= maxAllowed)
			{
				currentUpgrades.Add(selected);
			}
		}
	}

	public void closeShop()
	{
		game.subjects.decLives(spentLives);
		spentLives = 0;
	}
}
