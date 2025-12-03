using TheSeer.Models.Enums;
using TheSeer.Services;

namespace TheSeer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var tarotService = new TarotService();
            Console.WriteLine("Cards loaded successfully!");
        }
    }
}
