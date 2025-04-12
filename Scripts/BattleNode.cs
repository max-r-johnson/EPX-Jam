using Godot;

public partial class BattleNode : Node
{
    public override void _Ready()
    {
        Unit murderer = new Murderer();
        Subjects subjects = new Subjects([murderer]);
        GD.Print(subjects.ToString());
        instantiateUnits(subjects);
    }

    public void instantiateUnits(Subjects subjects)
    {
        var unitScene = GD.Load<PackedScene>("res://Scenes/Unit.tscn");
        foreach (Unit unit in subjects.units)
        {
            foreach (int i in GD.Range(subjects.unitQuantities[unit]))
            {
                var unitInstance = unitScene.Instantiate();
                unitInstance.Name = unit.GetType().Name;
                AddChild(unitInstance);
            }
        }
    }
}