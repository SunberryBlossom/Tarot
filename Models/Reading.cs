using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using TheSeer.Models.Enums;

namespace TheSeer.Models
{
    internal class Reading
    {
        public Guid Id { get; init; }
        public Guid UserId { get; init; }
        public SpreadType Type { get; init; }
        public DeckType Deck { get; init; }
        public List<Card> Cards { get; init; }
        public DateTime Timestamp { get; init; }
        public string? Question { get; init; }

        public Reading(Guid userId, SpreadType type, DeckType deck, List<Card> cards, string? question = null)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            Type = type;
            Deck = deck;
            
            if (cards == null || cards.Count == 0)
                throw new ArgumentException("A reading must have at least one card", nameof(cards));
            
            Cards = cards;
            Timestamp = DateTime.Now;
            Question = question;
        }

        public Reading() 
        {
            Cards = new List<Card>();
            Question = null;
        }
    }
}
