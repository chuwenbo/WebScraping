using System;
using System.IO;

namespace Scraping.FOREX.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            EconomicCalendarHandler economicCalendarHandler = new EconomicCalendarHandler();  
            economicCalendarHandler.Run();

            Console.WriteLine("Hello World!");
            Console.ReadKey();
        }
    }
}
