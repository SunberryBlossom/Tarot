using TheSeer.Managers;
using TheSeer.Services;

namespace TheSeer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tarotService = new TarotService();
            Console.WriteLine("Cards loaded successfully!");

            // Create services
            var dataService = new JsonDataService();
            var cryptoService = new CryptographyService();
            var validationService = new ValidationService();

            // Create manager with dependencies
            var userManager = new UserManager(dataService, cryptoService, validationService);

            // Now you can use userManager for registration and login
            Console.WriteLine("User management system initialized!");
        }
    }
}
