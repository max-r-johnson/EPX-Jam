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
	public override async void _Ready()
	{
        game.currentNode = this;
    }

    public void setupButtons()
	{
		Button refresh = GetNode<Button>("Refresh");
		refresh.Pressed += OnRefresh;

		Button closeShop = GetNode<Button>("Close Shop");
		closeShop.Pressed += OnCloseShop;
        
		Button item1 = GetNode<Button>("Button");
		item1.Pressed += OnItem1;

        Button item2 = GetNode<Button>("Button");
		item2.Pressed += OnItem2;

        Button item3 = GetNode<Button>("Button");
		item3.Pressed += OnItem3;

        Button item4 = GetNode<Button>("Button");
		item4.Pressed += OnItem4;

        Button item5 = GetNode<Button>("Button");
		item5.Pressed += OnItem5;

        Button item6 = GetNode<Button>("Button");
		item6.Pressed += OnItem6;
	}

	private void OnItem1()
	{
		
	}

    private void OnItem2()
	{
		
	}

    private void OnItem3()
	{
		
	}

    private void OnItem4()
	{
		
	}

    private void OnItem5()
	{
		
	}

    private void OnItem6()
	{
		
	}

	private void OnRefresh()
	{
		shop.spentLives += Shop.ROLL_AMOUNT;
		shop.refreshShop();
	}

	private void OnCloseShop()
	{
		shop.closeShop();
	}
}