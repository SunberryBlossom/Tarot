using TheSeer2.Models.Enums;
using TheSeer2.Services;

namespace TheSeer2
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

