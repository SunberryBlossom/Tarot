using System;
using TheSeer.Controllers;
using TheSeer.Managers;
using TheSeer.Services;
using TheSeer.Utilities.Helpers;

namespace TheSeer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize all services with dependency injection
            var dataService = new JsonDataService();
            var cryptoService = new CryptographyService();
            var validationService = new ValidationService();
            var narrator = new Narrator();
            var tarotService = new TarotService();
            var spreadService = new SpreadService();

            // Initialize managers
            var userManager = new UserManager(dataService, cryptoService, validationService);
            var readingManager = new ReadingManager(tarotService, spreadService, dataService );

            // Create and run the application controller
            var app = new Controllers.TheSeer(userManager, readingManager, narrator);
            
            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.Clear();
                ConsoleHelper.WriteError("An unexpected error occurred:");
                Console.WriteLine(ex.Message);
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
