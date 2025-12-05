using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using TheSeer.Interfaces;
using TheSeer.Models;

namespace TheSeer.Services
{
    internal class JsonDataService : IDataService
    {
        private readonly string _dataDirectory;           // runtime data (output folder)
        private readonly string _usersFilePath;
        private readonly string _readingsFilePath;

        // optional repo data copy (project root Data folder) — null when not found
        private readonly string? _repoDataDirectory;

        private List<User> _users;
        private List<Reading> _readings;

        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = true
        };

        public JsonDataService()
        {
            // runtime data lives in the app base directory (output folder)
            _dataDirectory = Path.Combine(AppContext.BaseDirectory, "Data");
            Directory.CreateDirectory(_dataDirectory);

            _usersFilePath = Path.Combine(_dataDirectory, "users.json");
            _readingsFilePath = Path.Combine(_dataDirectory, "readings.json");

            EnsureFileExists(_usersFilePath);
            EnsureFileExists(_readingsFilePath);

            // attempt to locate repository root (look for .git folder upwards from base dir)
            _repoDataDirectory = FindRepoDataDirectory(AppContext.BaseDirectory);

            // create repo Data folder if we found repo and it doesn't exist
            if (_repoDataDirectory != null)
            {
                try
                {
                    Directory.CreateDirectory(_repoDataDirectory);
                }
                catch
                {
                    // ignore permission issues; copy-back will be best-effort
                    _repoDataDirectory = null;
                }
            }

            _users = LoadFromFile<User>(_usersFilePath);
            _readings = LoadFromFile<Reading>(_readingsFilePath);
        }

        public User? GetUser(string username)
        {
            return _users
                .Where(u => u.Username != null)
                .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
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

                // atomic-ish write: write to temp then replace
                string tmpPath = filePath + ".tmp";
                File.WriteAllText(tmpPath, jsonText);
                File.Copy(tmpPath, filePath, overwrite: true);
                File.Delete(tmpPath);

                Log($"Successfully saved {items.Count} items to {filePath}");

                // attempt to copy runtime-updated file back to repo Data folder (best-effort)
                TryCopyToRepo(filePath);
            }
            catch (Exception ex)
            {
                Log($"Error saving to {filePath}: {ex.Message}");
            }
        }

        private void EnsureFileExists(string filePath)
        {
            if (!File.Exists(filePath))
            {
                try
                {
                    File.WriteAllText(filePath, "[]");
                    Log($"Created empty data file at {filePath}");
                }
                catch (Exception ex)
                {
                    Log($"Failed creating data file {filePath}: {ex.Message}");
                }
            }
        }

        /// <summary>
        /// Attempts to find the repository root by walking up from startPath and looking for a '.git' folder,
        /// then returns the 'Data' directory path inside that repo if found, otherwise null.
        /// </summary>
        private static string? FindRepoDataDirectory(string startPath)
        {
            try
            {
                var dir = new DirectoryInfo(startPath);
                while (dir != null)
                {
                    var gitPath = Path.Combine(dir.FullName, ".git");
                    if (Directory.Exists(gitPath) || File.Exists(gitPath)) // git could be a file (worktrees) — accept either
                    {
                        return Path.Combine(dir.FullName, "Data");
                    }

                    dir = dir.Parent;
                }
            }
            catch (Exception)
            {
                // swallow — repo detection is best-effort only
            }

            return null;
        }

        /// <summary>
        /// Copies a runtime-updated data file back to the repo Data folder (if present).
        /// Best-effort: logs and swallows exceptions so runtime behavior is unaffected.
        /// </summary>
        private void TryCopyToRepo(string runtimeFilePath)
        {
            if (string.IsNullOrWhiteSpace(_repoDataDirectory))
                return;

            try
            {
                var fileName = Path.GetFileName(runtimeFilePath);
                if (string.IsNullOrEmpty(fileName))
                    return;

                var repoFilePath = Path.Combine(_repoDataDirectory, fileName);

                // Make a temp copy and replace to reduce partial-write risk
                string tmpRepo = repoFilePath + ".tmp";
                File.Copy(runtimeFilePath, tmpRepo, overwrite: true);
                File.Copy(tmpRepo, repoFilePath, overwrite: true);
                File.Delete(tmpRepo);

                Log($"Copied runtime file '{runtimeFilePath}' to repo '{repoFilePath}'");
            }
            catch (Exception ex)
            {
                Log($"Failed to copy runtime file to repo: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            Console.WriteLine($"[JsonDataService] {DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}");
        }
    }
}
