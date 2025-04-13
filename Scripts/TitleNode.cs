using Godot;

public partial class TitleNode : Node
{
	public override void _Ready()
	{
		setupButtons();
	}

	public void setupButtons()
	{
		Button start = GetChildren()[2].GetNode<Button>("Start");
		start.Pressed += OnStart;
	}

	private void OnStart()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Intro.tscn");
	}
}
