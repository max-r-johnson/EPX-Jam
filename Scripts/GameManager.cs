using Godot;

public partial class GameManager : Node
{
    public static Game Game { get; private set; }

    public override void _Ready()
    {
        Game = new Game();
    }
}