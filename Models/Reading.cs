using System;
using System.Collections.Generic;
using TheSeer.Models.Enums;

namespace TheSeer.Models
{
    internal class Reading
    {
        // ----------------------- PROPERTIES --------------------------//
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public ReadingType Type { get; init; }
        public DeckType Deck { get; init; }
        public List<Card> Cards { get; init; }
        public DateTime Timestamp { get; init; }

        // ---------------- CONSTRUCTORS -----------------------------//
        
        public Reading(Guid userId, ReadingType type, DeckType deck, List<Card> cards)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Deck = deck;
            
            if (cards == null || cards.Count == 0)
                throw new ArgumentException("A reading must have at least one card", nameof(cards));
            
            Cards = cards;
            Timestamp = DateTime.Now;
        }

        public Reading() 
        {
            Cards = new List<Card>();
        }
    }
}
