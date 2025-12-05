using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TheSeer.Controllers;
using TheSeer.Models;
using TheSeer.Models.Enums;
using TheSeer.Utilities.Helpers;

namespace TheSeer.UI.Menus
{
    internal class ReadingHistoryMenu : BaseMenu
    {
        private readonly Controllers.TheSeer _app;
        private List<string> _menuOptions = new();
        private List<Reading> _allReadings = new();

        // Active filter criteria (can be combined)
        private class FilterCriteria
        {
            public ReadingType? Type { get; set; }
            public DeckType? Deck { get; set; }
            public DateTime? StartDate { get; set; }
            public DateTime? EndDate { get; set; }

            public void Clear()
            {
                Type = null;
                Deck = null;
                StartDate = null;
                EndDate = null;
            }
        }

        private readonly FilterCriteria _criteria = new();

        public ReadingHistoryMenu(Controllers.TheSeer app) : base(app.Narrator)
        {
            _app = app ?? throw new ArgumentNullException(nameof(app));
        }

        protected override List<string> GetMenuOptions() => _menuOptions;

        public override bool Show()
        {
            var currentUser = _app.UserManager.GetCurrentUser();
            if (currentUser == null)
            {
                ShowError("No user is currently logged in.");
                return false;
            }

            // Load once for the session and reuse
            _allReadings = _app.ReadingManager.GetUserReadingHistory(currentUser.Id) ?? new List<Reading>();

            ShowHeader("Reading History");

            if (_allReadings.Count == 0)
            {
                _narrator.SpeakWisdom("You have no recorded readings yet, traveler.");
                PressAnyKey();
                return false;
            }

            while (true)
            {
                BuildMainMenuOptions();
                int choice = ShowTemporaryMenu("Reading History", _menuOptions, GetCriteriaSummary());

                switch (choice)
                {
                    case 1: // View Readings (apply current filters)
                        var results = ApplyFilters();
                        ShowReadingsPaged(results, "Readings");
                        break;

                    case 2: // Modify Filters
                        EditFilters();
                        break;

                    case 3: // Clear Filters
                        _criteria.Clear();
                        _narrator.SpeakWisdom("Filters cleared.");
                        PressAnyKey();
                        break;

                    case 4: // Back / Escape
                    case 0:
                        return false;

                    default:
                        ShowError("Unexpected selection. Try again.");
                        break;
                }
            }
        }

        // Build top-level menu
        private void BuildMainMenuOptions()
        {
            _menuOptions = new List<string>
            {
                "View Readings (apply current filters)",
                "Modify Filters",
                "Clear Filters",
                "Back"
            };
        }

        private string GetCriteriaSummary()
        {
            var parts = new List<string>();
            if (_criteria.Type.HasValue) parts.Add($"Type={_criteria.Type.Value}");
            if (_criteria.Deck.HasValue) parts.Add($"Deck={_criteria.Deck.Value}");
            if (_criteria.StartDate.HasValue) parts.Add($"From={_criteria.StartDate:yyyy-MM-dd}");
            if (_criteria.EndDate.HasValue) parts.Add($"To={_criteria.EndDate:yyyy-MM-dd}");
            return parts.Count == 0 ? "No filters applied" : "Filters: " + string.Join(" | ", parts);
        }

        // Edit multiple filters in one session
        private void EditFilters()
        {
            while (true)
            {
                var options = new List<string>
                {
                    _criteria.Type.HasValue ? $"Reading Type: {_criteria.Type.Value}" : "Set Reading Type",
                    _criteria.Deck.HasValue ? $"Deck: {_criteria.Deck.Value}" : "Set Deck Type",
                    (_criteria.StartDate.HasValue && _criteria.EndDate.HasValue)
                        ? $"Date Range: {_criteria.StartDate:yyyy-MM-dd} → {_criteria.EndDate:yyyy-MM-dd}"
                        : "Set Date Range",
                    "View Results (with current filters)",
                    "Back"
                };

                int choice = ShowTemporaryMenu("Modify Filters", options, "Choose a filter to set/unset:");

                switch (choice)
                {
                    case 1: // Reading Type
                    {
                        var (cancelled, selected) = PromptForReadingType();
                        if (!cancelled)
                        {
                            // selected == null means user chose Unset
                            _criteria.Type = selected;
                        }
                        break;
                    }

                    case 2: // Deck Type
                    {
                        var (cancelled, selectedDeck) = PromptForDeckType();
                        if (!cancelled)
                        {
                            _criteria.Deck = selectedDeck;
                        }
                        break;
                    }

                    case 3: // Date range
                    {
                        var (cancelled, start, end, cleared) = PromptForDateRange();
                        if (cancelled)
                        {
                            // do nothing
                        }
                        else if (cleared)
                        {
                            _criteria.StartDate = null;
                            _criteria.EndDate = null;
                        }
                        else
                        {
                            _criteria.StartDate = start?.Date;
                            _criteria.EndDate = end?.Date;
                        }
                        break;
                    }

                    case 4: // View results
                    {
                        var results = ApplyFilters();
                        ShowReadingsPaged(results, "Filtered Readings");
                        break;
                    }

                    case 5: // Back
                    case 0:
                        return;

                    default:
                        ShowError("Unexpected selection. Try again.");
                        break;
                }
            }
        }

        // Apply combined filters and return ordered list
        private List<Reading> ApplyFilters()
        {
            IEnumerable<Reading> query = _allReadings;

            if (_criteria.Type.HasValue)
                query = query.Where(r => r.Type == _criteria.Type.Value);

            if (_criteria.Deck.HasValue)
                query = query.Where(r => r.Deck == _criteria.Deck.Value);

            if (_criteria.StartDate.HasValue)
            {
                var s = _criteria.StartDate.Value.Date;
                var e = (_criteria.EndDate ?? _criteria.StartDate.Value).Date;
                query = query.Where(r => r.Timestamp.Date >= s && r.Timestamp.Date <= e);
            }

            return query.OrderByDescending(r => r.Timestamp).ToList();
        }

        // Paged viewing of readings with Next/Prev navigation
        private void ShowReadingsPaged(List<Reading> readings, string title)
        {
            if (readings == null || readings.Count == 0)
            {
                ShowError("No readings match your selection.");
                return;
            }

            const int pageSize = 10;
            int page = 0;
            int totalPages = (int)Math.Ceiling(readings.Count / (double)pageSize);

            while (true)
            {
                var pageItems = readings.Skip(page * pageSize).Take(pageSize).ToList();

                var options = pageItems.Select(r =>
                {
                    string spreadName;
                    try
                    {
                        spreadName = _app.ReadingManager.GetSpread(r.Type).Name;
                    }
                    catch
                    {
                        spreadName = r.Type.ToString();
                    }

                    var time = r.Timestamp.ToString("yyyy-MM-dd HH:mm");
                    var questionPart = string.IsNullOrWhiteSpace(r.Question) ? "" : $" - \"{r.Question}\"";
                    return $"{time} - {spreadName} ({r.Type}) [{r.Deck}]{questionPart}";
                }).ToList();

                // navigation controls
                if (page > 0) options.Add("Previous Page");
                if (page < totalPages - 1) options.Add("Next Page");
                options.Add("Back");

                string pageHint = $"Page {page + 1}/{totalPages} - {readings.Count} results";
                int choice = ShowTemporaryMenu(title, options, pageHint);

                if (choice <= 0)
                    return;

                // calculate mapping of choice to actions
                int itemCount = pageItems.Count;
                bool hasPrev = page > 0;
                bool hasNext = page < totalPages - 1;

                if (choice <= itemCount)
                {
                    // user selected a reading
                    var selected = pageItems[choice - 1];
                    var spread = _app.ReadingManager.GetSpread(selected.Type);
                    _narrator.NarrateReading(selected, spread);
                    PressAnyKey();
                    // after viewing, remain on same page
                    continue;
                }

                int navIndex = choice - itemCount;
                if (hasPrev && navIndex == 1)
                {
                    page--;
                    continue;
                }

                if ((hasPrev && hasNext && navIndex == 2) || (!hasPrev && hasNext && navIndex == 1))
                {
                    page++;
                    continue;
                }

                // Back option is the last
                return;
            }
        }

        // Prompts without permanently mutating _menuOptions
        private int ShowTemporaryMenu(string title, List<string> options, string? prompt = null)
        {
            var old = _menuOptions;
            try
            {
                _menuOptions = options;
                return ShowInteractiveMenu(title, prompt);
            }
            finally
            {
                _menuOptions = old;
            }
        }

        // Prompt for reading type. Returns (cancelled, selected) where cancelled==true means user chose Back,
        // selected==null (and cancelled==false) means "Unset filter".
        private (bool cancelled, ReadingType? selected) PromptForReadingType()
        {
            var types = Enum.GetValues(typeof(ReadingType)).Cast<ReadingType>().ToList();
            var options = types.Select(t => t.ToString()).ToList();
            options.Add("Unset filter");
            options.Add("Back");

            int choice = ShowTemporaryMenu("Filter by Spread Type", options, "Choose the spread type (or Unset):");

            if (choice <= 0 || choice > options.Count)
                return (true, null);

            if (choice == types.Count + 1) // Unset
                return (false, null);

            if (choice == types.Count + 2) // Back
                return (true, null);

            return (false, types[choice - 1]);
        }

        // Prompt for deck type. Returns (cancelled, selectedDeck) same semantics as PromptForReadingType.
        private (bool cancelled, DeckType? selected) PromptForDeckType()
        {
            var decks = Enum.GetValues(typeof(DeckType)).Cast<DeckType>().ToList();
            var options = decks.Select(d => d.ToString()).ToList();
            options.Add("Unset filter");
            options.Add("Back");

            int choice = ShowTemporaryMenu("Filter by Deck Type", options, "Choose the deck (or Unset):");

            if (choice <= 0 || choice > options.Count)
                return (true, null);

            if (choice == decks.Count + 1) // Unset
                return (false, null);

            if (choice == decks.Count + 2) // Back
                return (true, null);

            return (false, decks[choice - 1]);
        }

        // Prompt for date range. Returns (cancelled, start, end, cleared)
        // cancelled == true -> user cancelled/back
        // cleared == true -> user chose to unset the date filter
        private (bool cancelled, DateTime? start, DateTime? end, bool cleared) PromptForDateRange()
        {
            while (true)
            {
                ShowHeader("Filter by Date Range");
                ConsoleHelper.TypeWriteLine("Enter the start date (YYYY-MM-DD), 'clear' to unset, or 'back' to cancel:", ConsoleHelper.Colors.Mystical, 1);
                ConsoleHelper.WriteBlankLine();
                string startInput = ConsoleHelper.ReadLine("Start date: ", ConsoleHelper.Colors.Info).Trim();

                if (string.Equals(startInput, "back", StringComparison.OrdinalIgnoreCase))
                    return (true, null, null, false);

                if (string.Equals(startInput, "clear", StringComparison.OrdinalIgnoreCase))
                    return (false, null, null, true);

                if (!DateTime.TryParseExact(startInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime startDate))
                {
                    ShowError("Invalid start date. Use format YYYY-MM-DD.");
                    continue;
                }

                ConsoleHelper.TypeWriteLine("Enter the end date (YYYY-MM-DD) or leave empty to use the same day (type 'back' to cancel):", ConsoleHelper.Colors.Mystical, 1);
                ConsoleHelper.WriteBlankLine();
                string endInput = ConsoleHelper.ReadLine("End date: ", ConsoleHelper.Colors.Info).Trim();

                if (string.Equals(endInput, "back", StringComparison.OrdinalIgnoreCase))
                    return (true, null, null, false);

                DateTime endDate;
                if (string.IsNullOrWhiteSpace(endInput))
                {
                    endDate = startDate;
                }
                else if (!DateTime.TryParseExact(endInput, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
                {
                    ShowError("Invalid end date. Use format YYYY-MM-DD.");
                    continue;
                }

                if (endDate < startDate)
                {
                    ShowError("End date cannot be earlier than start date.");
                    continue;
                }

                return (false, startDate.Date, endDate.Date, false);
            }
        }
    }
}
