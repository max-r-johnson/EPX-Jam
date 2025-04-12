using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class Subjects
{
    public Dictionary<Unit, int> unitQuantities;
    public List<Unit> units;
    public Subjects(List<Unit> units)
    {
        this.units = units;
        this.unitQuantities = units.ToDictionary(unit => unit, unit => unit.quantity);
    }

    public override string ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        foreach (var pair in unitQuantities)
        {
            stringBuilder.Append(pair.Key + ": " + pair.Value + "\n");
        }
        return stringBuilder.ToString();
    }
}
