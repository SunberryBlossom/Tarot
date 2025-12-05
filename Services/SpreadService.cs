using System;
using System.Collections.Generic;
using System.Linq;
using TheSeer.Managers;
using TheSeer.Models;
using TheSeer.Models.Enums;

namespace TheSeer.Services;

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
                    DeckType.Tarot, // <- deck specified
                    "Daily Reading",
                    "This, my friend, is a chance to glimpse your day ahead of it happening. One card, for one day. What say you, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The One Light", "In this reading, that may only be offered once per day, the One Light will illuminate what's ahead of you; good or evil, this will show what awaits you.")
                    }
                )
            },
            {
                ReadingType.Wanderer,
                new Spread
                (
                    ReadingType.Wanderer,
                    DeckType.Tarot,
                    "The Wanderer",
                    "The Wanderer is a lonely spread. A single card, to answer a single, yet perhaps not simple question. She may help you with a conundrum; a specific, yet not closed question... Would you like to walk with the Wanderer, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The Wanderer's hand", "This card is here to give you a chance of grabbing your clarity. Do not wander alone in the darkness, child, when light is in the Wanderer's hand.")
                    }
                )
            },
            {
                ReadingType.Nuns,
                new Spread
                (
                    ReadingType.Nuns,
                    DeckType.Tarot,
                    "The Two Nuns",
                    "The sisterly nuns may show you duality. A chance to differ what one ought to do, and what one ought to avoid. Their holy guidance will stop you from devilish actions, whilst leading you to more pious lands. Do you desire holy guidance from the two nuns, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The Nun of Warning", "She is the stern sister, who warns you of paths that lead to darkness. Her card reveals what you must avoid, what temptations lurk, and what actions would lead you astray. Heed her warning, dear child."),
                        new(1, "The Nun of Encouragement", "She is the gentle sister, who blesses your righteous path. Her card illuminates what you should embrace, what virtues to cultivate, and what actions will lead you to grace. Follow her light, and prosper.")
                    }
                )
            },
            {
                ReadingType.Spies,
                new Spread
                (
                    ReadingType.Spies,
                    DeckType.Tarot,
                    "The Three Spies",
                    "Three shadowy spies I have at my disposal. These men do not spy in courts, but in fate, and destiny. One gathers intel from your past, the other of your present, the third of your future. These three spies are for hire. What do you say, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The Spy of the Past", "This spy has infiltrated your history, uncovering the foundations upon which you stand. His intelligence reveals the lessons learned, the wounds carried, and the patterns that still echo in your present. Listen to what he has discovered in the shadows of yesterday."),
                        new(1, "The Spy of the Present", "This spy observes you in the now, hidden in plain sight. He sees the currents that move through your life today, the energies surrounding you, and the truth of your current situation that you may not yet perceive. His report is urgent and immediate."),
                        new(2, "The Spy of the Future", "This spy has ventured ahead into possibilities yet unformed. He brings word of what approaches, what potential paths lie before you, and what you might encounter if you continue on your current trajectory. His intelligence is a glimpse of what may come to pass.")
                    }
                )
            },
            {
                ReadingType.Lords,
                new Spread
                (
                    ReadingType.Lords,
                    DeckType.Tarot,
                    "The Four Lords",
                    "The four lords rule over fiefs, but not fiefs of land... they oversee the four corners of our being: mind, body, spirit and subconsciousness. If you desire, the lords might come to sit with us, to tell you what is going on in these four areas of yourself. Will you meet with the four noble lords of your life, Traveler?",
                    new List<SpreadPosition>
                    {
                        new(0, "The Lord of Mind", "This lord governs your thoughts, intellect, and conscious reasoning. His domain reveals your mental clarity, the ideas occupying your awareness, and the logic that guides your decisions. What state is your kingdom of thought in, dear seeker?"),
                        new(1, "The Lord of Body", "This lord presides over your physical form and material world. His realm shows your health, vitality, earthly concerns, and the tangible manifestations of your existence. How fares your corporeal kingdom, child?"),
                        new(2, "The Lord of Spirit", "This lord watches over your soul's journey and divine connection. His territory encompasses your spiritual truth, higher purpose, and the sacred essence that transcends the physical. What does your spiritual lord wish you to know?"),
                        new(3, "The Lord of Subconsciousness", "This lord rules the hidden depths beneath awareness. His shadowy domain holds your dreams, instincts, buried memories, and the truths you hide even from yourself. What secrets does this mysterious lord reveal from the depths of your psyche?")
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
