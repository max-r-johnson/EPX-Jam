using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public partial class ShopNode : Node
{
	public Game game {get {return GameManager.Game;}}
	public Shop shop {get {return game.shop;}}
	public Subjects subjects {get {return game.subjects;}}
	public Subjects enemySubjects {get {return game.enemySubjects;}}
	public List<Button> upgradeButtonList { get; set; } = [];
	public Button refreshButton { get; set; }
	public override void _Ready()
	{
		game.currentNode = this;
		setupButtons();
		shop.refreshShop();
		changeShopDisplay();
	}

	public void setupButtons()
	{
		Button refresh = GetChildren()[2].GetNode<Button>("Refresh");
		refresh.Pressed += OnRefresh;
		refreshButton = refresh;
		refreshButton.Show();

		Button closeShop = GetNode<ColorRect>("ColorRect").GetNode<Button>("Close");
		closeShop.Pressed += OnCloseShop;

		Button upgrade1 = GetNode<TextureRect>("Pentagram").GetNode<ColorRect>("ColorRect1").GetNode<Button>("Upgrade1");
		upgrade1.Pressed += OnUpgrade1;
		upgradeButtonList.Add(upgrade1);

		Button upgrade2 = GetNode<TextureRect>("Pentagram").GetNode<ColorRect>("ColorRect2").GetNode<Button>("Upgrade2");
		upgrade2.Pressed += OnUpgrade2;
		upgradeButtonList.Add(upgrade2);

		Button upgrade3 = GetNode<TextureRect>("Pentagram").GetNode<ColorRect>("ColorRect3").GetNode<Button>("Upgrade3");
		upgrade3.Pressed += OnUpgrade3;
		upgradeButtonList.Add(upgrade3);

		Button upgrade4 = GetNode<TextureRect>("Pentagram").GetNode<ColorRect>("ColorRect4").GetNode<Button>("Upgrade4");
		upgrade4.Pressed += OnUpgrade4;
		upgradeButtonList.Add(upgrade4);

		Button upgrade5 = GetNode<TextureRect>("Pentagram").GetNode<ColorRect>("ColorRect5").GetNode<Button>("Upgrade5");
		upgrade5.Pressed += OnUpgrade5;
		upgradeButtonList.Add(upgrade5);
	}

	private void OnUpgrade1()
	{
		shop.spentLives += shop.currentUpgrades[0].cost;
		shop.currentUpgrades[0].upgradeMethod();
		removeUpgrade(0);
	}

	private void OnUpgrade2()
	{
		shop.spentLives += shop.currentUpgrades[1].cost;
		shop.currentUpgrades[1].upgradeMethod();
		removeUpgrade(1);

	}

	private void OnUpgrade3()
	{
		shop.spentLives += shop.currentUpgrades[2].cost;
		shop.currentUpgrades[2].upgradeMethod();
		removeUpgrade(2);
		
	}

	private void OnUpgrade4()
	{
		shop.spentLives += shop.currentUpgrades[3].cost;
		shop.currentUpgrades[3].upgradeMethod();	
		removeUpgrade(3);
	
	}

	private void OnUpgrade5()
	{
		shop.spentLives += shop.currentUpgrades[4].cost;
		shop.currentUpgrades[4].upgradeMethod();
		removeUpgrade(4);
	}

	public void changeTexture(int i, string text)
	{
		Game.setNodeTexture(upgradeButtonList[i], text, new Vector2(60, 60));
	}

	public void changeDescription(int i)
	{
		Button node = upgradeButtonList[i];
		node.Show();
		node.GetNode<Label>("Label").Show();
		node.GetNode<Label>("SecretLabel").Hide();
		string upgradeName = shop.currentUpgrades[i]?.title ?? shop.currentUpgrades[i].GetType().Name;
		if (upgradeName.EndsWith("Upgrade"))
		{
			upgradeName = upgradeName.Substring(0, upgradeName.Length - "Upgrade".Length) + "+";
		}
		if (upgradeName.EndsWith("First"))
		{
			upgradeName = upgradeName.Substring(0, upgradeName.Length - "First".Length);
		}
		if(upgradeName == "Anubis's Scale")
		{
			node.GetNode<Label>("Label").Hide();
			node.GetNode<Label>("SecretLabel").Show();
			node.GetNode<Label>("SecretLabel").Text = upgradeName;
		}
		node.GetNode<Label>("Label").Text = upgradeName;
		node.GetNode<Label>("Label2").Text = shop.currentUpgrades[i].text;
		node.GetNode<Label>("Label3").Text = shop.currentUpgrades[i].cost.ToString();
	}

	public void changeTotalLives()
	{
		int totalLives = game.subjects.totalInstances() - shop.spentLives;
		refreshButton.GetNode<Label>("Label").Text = totalLives.ToString() + " Lives";
		foreach(int i in GD.Range(shop.currentUpgrades.Count))
		{
			if(totalLives <= shop.currentUpgrades[i].cost)
			{
				upgradeButtonList[i].Hide();
			}
		}
		if(totalLives <= Shop.ROLL_AMOUNT)
		{
			refreshButton.Hide();
		}
	}

	public void removeUpgrade(int i)
	{
		upgradeButtonList[i].Hide();
		changeTotalLives();
	}

	public void changeShopDisplay()
	{
		foreach(int i in GD.Range(upgradeButtonList.Count))
		{
			changeTexture(i, shop.currentUpgrades[i].GetType().Name);
			changeDescription(i);
		}
		changeTotalLives();
	}

	private void OnRefresh()
	{
		shop.spentLives += Shop.ROLL_AMOUNT;
		shop.refreshShop();
		changeShopDisplay();
	}

	private void OnCloseShop()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Battle.tscn");
	}
}
