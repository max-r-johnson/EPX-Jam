using Godot;

public partial class IntroNode : Node
{
	public override void _Ready()
	{
		setupButtons();
	}

	public void setupButtons()
	{
		Button begin = GetChildren()[0].GetChildren()[0].GetChildren()[2].GetNode<Button>("Begin");
		begin.Pressed += OnBegin;
	}

	private void OnBegin()
	{
		GetTree().ChangeSceneToFile("res://Scenes/Battle.tscn");
	}
}
