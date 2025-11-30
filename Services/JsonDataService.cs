using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TheSeer2.Interfaces;
using TheSeer2.Models;

namespace TheSeer2.Services
{
    internal class JsonDataService : IDataService
    {
        private readonly string _usersFilePath = "Data/users.json";
        private readonly string _readingsFilePath = "Data/readings.json";

        private List<User> _users;
        private List<Reading> _readings;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public JsonDataService()
        {
            Directory.CreateDirectory("Data");

            _users = LoadFromFile<User>(_usersFilePath);
            _readings = LoadFromFile<Reading>(_readingsFilePath);
        }

        public User? GetUser(string username)
        {
            return _users.FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }

        public void SaveUser(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);

            if (existingUser != null)
            {
                _users.Remove(existingUser);
            }

            _users.Add(user);

            SaveToFile(_users, _usersFilePath);
        }

        public List<User> GetAllUsers()
        {
            return new List<User>(_users);
        }

        public void SaveReading(Reading reading)
        {
            _readings.Add(reading);
            SaveToFile(_readings, _readingsFilePath);
        }

        public List<Reading> GetUserReadings(Guid userId)
        {
            return _readings.Where(r => r.UserId == userId)
                           .OrderByDescending(r => r.Timestamp)
                           .ToList();
        }

        private List<T> LoadFromFile<T>(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Log($"File not found: {filePath}. Creating new empty list.");
                return new List<T>();
            }

            try
            {
                string jsonText = File.ReadAllText(filePath);
                var items = JsonSerializer.Deserialize<List<T>>(jsonText);
                
                Log($"Successfully loaded {items?.Count ?? 0} items from {filePath}");
                return items ?? new List<T>();
            }
            catch (Exception ex)
            {
                Log($"Error loading {filePath}: {ex.Message}");
                return new List<T>();
            }
        }

        private void SaveToFile<T>(List<T> items, string filePath)
        {
            try
            {
                string jsonText = JsonSerializer.Serialize(items, _jsonOptions);
                File.WriteAllText(filePath, jsonText);
                Log($"Successfully saved {items.Count} items to {filePath}");
            }
            catch (Exception ex)
            {
                Log($"Error saving to {filePath}: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            Console.WriteLine($"[JsonDataService] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
