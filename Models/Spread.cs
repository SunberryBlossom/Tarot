using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using TheSeer.Models.Enums;

namespace TheSeer.Models;

internal class Spread
{
    public ReadingType Type { get; init; }
    public int CardCount { get; init; }
    public string Name { get; init; }
    public string Description { get; init; }
    public ReadOnlyCollection<SpreadPosition> Positions { get; init; }

    public Spread(ReadingType type, string name, string description, List<SpreadPosition> positions)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Spread name cannot be empty", nameof(name));
        
        if (string.IsNullOrWhiteSpace(description))
            throw new ArgumentException("Spread description cannot be empty", nameof(description));
        
        if (positions == null)
            throw new ArgumentNullException(nameof(positions));
        
        if (positions.Count == 0)
            throw new ArgumentException("A spread must have at least one position", nameof(positions));
        
        Type = type;
        Name = name;
        Description = description;
        Positions = positions.AsReadOnly();
        CardCount = positions.Count;
    }

    public override string ToString() => 
        $"{Name} ({Type}): {CardCount} card(s)";
}

internal record SpreadPosition(int Index, string Name, string Meaning);
