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
        unitQuantities = units.ToDictionary(unit => unit, unit => unit.quantity);
    }

    public void incLives(int amount)
    {
        var currentWeights = new Dictionary<Unit, int>(unitQuantities);

        int total = currentWeights.Values.Sum();

        foreach (int i in GD.Range(amount))
        {
            int randomValue = GD.RandRange(0, total-1);
            int cumulative = 0;

            foreach (var pair in currentWeights)
            {
                cumulative += pair.Value;
                if (randomValue < cumulative)
                {
                    addUnit(pair.Key);
                    break;
                }
            }
        }
    }

// Don't base off of existing weights, because there should be a larger chance the smaller group decrements after awhile
public void decLives(int amount)
{
    foreach (int i in GD.Range(amount))
    {
        int total = unitQuantities.Values.Sum();
        if (total == 0) break;

        int randomValue = GD.RandRange(0, total - 1);
        int cumulative = 0;

        foreach (var pair in unitQuantities)
        {
            cumulative += pair.Value;
            if (randomValue < cumulative)
            {
                removeUnit(pair.Key);
                break;
            }
        }
    }
}

    public void addUnit(Unit unit)
    {
        unitQuantities[unit] += 1;
    }

    public void removeUnit(Unit unit)
    {
        unitQuantities[unit] -= 1;
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
