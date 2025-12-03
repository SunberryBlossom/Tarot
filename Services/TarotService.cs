using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TheSeer.Models;
using TheSeer.Models.Enums;

namespace TheSeer.Services
{
    internal class TarotService
    {
        private readonly Random _random = new();
        private readonly string _cardsFilePath = "Data/cards.json";
        private List<Card> _allCards;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true,
            Converters = { new JsonStringEnumConverter() }
        };

        public TarotService()
        {
            _allCards = LoadCardsFromFile();
        }

        public List<Card> GetDeckCards(DeckType deck)
        {
            return _allCards.Where(c => c.Deck == deck).ToList();
        }

        public List<Card> DrawCards(DeckType deck, int count)
        {
            if (count <= 0)
                throw new ArgumentException("Must draw at least one card", nameof(count));

            var deckCards = GetDeckCards(deck);

            if (deckCards.Count < count)
                throw new InvalidOperationException($"{deck} deck only has {deckCards.Count} cards, cannot draw {count}");

            var availableCards = deckCards.Select(c => new Card
            {
                Id = c.Id,
                Name = c.Name,
                Deck = c.Deck,
                Suit = c.Suit,
                UprightMeaning = c.UprightMeaning,
                ReversedMeaning = c.ReversedMeaning
            }).ToList();

            Shuffle(availableCards);

            var drawnCards = availableCards.Take(count).ToList();

            foreach (var card in drawnCards)
            {
                if (_random.Next(2) == 1)
                {
                    card.SetReversed();
                }
            }

            return drawnCards;
        }

        public void Shuffle(List<Card> cards)
        {
            int n = cards.Count;
            while (n > 1)
            {
                n--;
                int k = _random.Next(n + 1);
                (cards[k], cards[n]) = (cards[n], cards[k]);
            }
        }

        public Card? GetCardById(int id)
        {
            return _allCards.FirstOrDefault(c => c.Id == id);
        }

        public int GetDeckCount(DeckType deck)
        {
            return _allCards.Count(c => c.Deck == deck);
        }

        private List<Card> LoadCardsFromFile()
        {
            try
            {
                string jsonText = File.ReadAllText(_cardsFilePath);
                var cards = JsonSerializer.Deserialize<List<Card>>(jsonText, _jsonOptions);

                Log($"Successfully loaded {cards?.Count ?? 0} cards from {_cardsFilePath}");
                return cards ?? new List<Card>();
            }
            catch (Exception ex)
            {
                Log($"Error loading cards: {ex.Message}");
                return new List<Card>();
            }
        }

        private void Log(string message)
        {
            Console.WriteLine($"[TarotService] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
