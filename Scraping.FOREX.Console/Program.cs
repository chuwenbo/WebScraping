using System;
using System.IO;

namespace Scraping.FOREX.Application
{
    class Program
    {
        static void Main(string[] args)
        {
            EconomicCalendarHandler economicCalendarHandler = new EconomicCalendarHandler();
            string token = economicCalendarHandler.GetAuthToken();
            string javascriptHtml = economicCalendarHandler.GetMainCalendar(token, "20180815", "20180816"); 

            //string html = File.ReadAllText("./sample/html-snippet.txt"); 

            Console.WriteLine("Hello World!");
        }
    }
}
