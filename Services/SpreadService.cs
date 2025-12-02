using System;
using System.Collections.Generic;
using System.Linq;
using TheSeer2.Managers;
using TheSeer2.Models;
using TheSeer2.Models.Enums;

namespace TheSeer2.Services;

internal class SpreadService
{
    private readonly Dictionary<ReadingType, Spread> _spreads;

    public SpreadService()
    {
        _spreads = InitializeSpreads();
    }

    private Dictionary<ReadingType, Spread> InitializeSpreads()
    {
        return new Dictionary<ReadingType, Spread>
        {
            {
                ReadingType.DailyReading,
                new Spread
                (
                    ReadingType.DailyReading,
                    "Daily Reading",
                    "This, my friend, is a chance to glimpse your day ahead of it happening. One card, for one day. What say you, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The One Light", "In this reading, that may only be offered once per day, the One Light will illuminate what's ahead of you; good or evil, this will show what awaits you.")
                    }
                )
            }
        };
    }

    public Spread GetSpread(ReadingType type)
    {
        if (_spreads.TryGetValue(type, out var spread))
            return spread;

        throw new ArgumentException($"No spread defined for {type}", nameof(type));
    }

    public bool TryGetSpread(ReadingType type, out Spread? spread)
    {
        return _spreads.TryGetValue(type, out spread);
    }

    public IEnumerable<Spread> GetAllSpreads() => _spreads.Values;

    public IEnumerable<string> GetSpreadNames() => _spreads.Values.Select(s => s.Name);

    public int GetCardCount(ReadingType type)
    {
        return GetSpread(type).CardCount;
    }
}
