using System;
using TheSeer.Models.Enums;

namespace TheSeer.Models
{
    internal class Card
    {
        // ----------------------- PROPERTIES --------------------------//
        public int Id { get; init; }
        public required string Name { get; init; }
        public DeckType Deck { get; init; }
        public CardSuit Suit { get; init; }
        public required string UprightMeaning { get; init; }
        public required string ReversedMeaning { get; init; }
        public bool IsReversed { get; set; }

        // ---------------- CONSTRUCTORS -----------------------------//

        public Card(int id, string name, DeckType deck, CardSuit suit, string uprightMeaning, string reversedMeaning)
        {
            Id = id;
            Name = name;
            Deck = deck;
            Suit = suit;
            UprightMeaning = uprightMeaning;
            ReversedMeaning = reversedMeaning;
            IsReversed = false;
        }

        public Card()
        {
            Name = string.Empty;
            UprightMeaning = string.Empty;
            ReversedMeaning = string.Empty;
        }

        // --------------- METHODS -----------------------//

        public string GetMeaning()
        {
            return IsReversed ? ReversedMeaning : UprightMeaning;
        }

        public void SetReversed()
        {
            IsReversed = true;
        }

        public void SetUpright()
        {
            IsReversed = false;
        }
    }
}
