using System;
using System.Collections.Generic;
using System.Linq;
using TheSeer.Interfaces;
using TheSeer.Models;
using TheSeer.Models.Enums;
using TheSeer.Services;

namespace TheSeer.Managers
{
    internal class ReadingManager
    {
        private readonly TarotService _tarotService;
        private readonly SpreadService _spreadService;
        private readonly IDataService _dataService;

        public ReadingManager(TarotService tarotService, SpreadService spreadService, IDataService dataService)
        {
            _tarotService = tarotService;
            _spreadService = spreadService;
            _dataService = dataService;
        }

        public Reading CreateReading(Guid userId, ReadingType readingType, DeckType deck)
        {
            ValidateUserId(userId);

            // Business rule: Daily reading restriction
            if (readingType == ReadingType.DailyReading && !CanPerformDailyReading(userId))
                throw new InvalidOperationException("You have already performed a daily reading today. Please try again tomorrow.");

            var spread = _spreadService.GetSpread(readingType);
            var drawnCards = _tarotService.DrawCards(deck, spread.CardCount);
            var reading = new Reading(userId, readingType, deck, drawnCards);
            
            _dataService.SaveReading(reading);

            return reading;
        }

        public bool CanPerformDailyReading(Guid userId)
        {
            ValidateUserId(userId);

            var userReadings = _dataService.GetUserReadings(userId);
            var today = DateTime.Today;
            
            return !userReadings.Any(r => 
                r.Type == ReadingType.DailyReading && 
                r.Timestamp.Date == today);
        }

        private void ValidateUserId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("User ID cannot be empty", nameof(userId));
        }

        public List<Reading> GetUserReadingHistory(Guid userId)
        {
            ValidateUserId(userId);
            return _dataService.GetUserReadings(userId);
        }

        public Reading? GetReadingById(Guid userId, Guid readingId)
        {
            ValidateUserId(userId);

            if (readingId == Guid.Empty)
                throw new ArgumentException("Reading ID cannot be empty", nameof(readingId));

            var userReadings = _dataService.GetUserReadings(userId);
            return userReadings.FirstOrDefault(r => r.Id == readingId);
        }

        public List<Reading> GetReadingsByType(Guid userId, ReadingType type)
        {
            ValidateUserId(userId);

            var userReadings = _dataService.GetUserReadings(userId);
            return userReadings.Where(r => r.Type == type).ToList();
        }

        public Spread GetSpread(ReadingType readingType)
        {
            return _spreadService.GetSpread(readingType);
        }

        public IEnumerable<Spread> GetAllAvailableSpreads()
        {
            return _spreadService.GetAllSpreads();
        }

        public int GetTotalReadingCount(Guid userId)
        {
            ValidateUserId(userId);

            return _dataService.GetUserReadings(userId).Count;
        }

        public DateTime? GetLastReadingDate(Guid userId)
        {
            ValidateUserId(userId);
            var readings = _dataService.GetUserReadings(userId);
            return readings.Max(r => (DateTime?)r.Timestamp);
        }

        public Dictionary<ReadingType, int> GetReadingCountByType(Guid userId)
        {
            ValidateUserId(userId);

            var readings = _dataService.GetUserReadings(userId);
            
            if (!readings.Any())
                return new Dictionary<ReadingType, int>();

            return readings
                .GroupBy(r => r.Type)
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
