using HtmlAgilityPack;
using Newtonsoft.Json;
using Scraping.FOREX.Application.Model;
using Scraping.Web.Entities;
using Scraping.Web.Entities.FOREX;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using System.Threading;

namespace Scraping.FOREX.Application
{
    public class EconomicCalendarHandler
    {
        public string GetAuthToken()
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create("http://authorization.fxstreet.com/token");
                 
                var data = Encoding.ASCII.GetBytes("grant_type=domain&client_id=client_id"); 
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Accept = "ation/json, text/javascript, */*; q=0.01";
                request.ContentLength = data.Length;
                request.Headers["Origin"] = "https://www.forex.com";
                request.Referer = "https://www.forex.com/cn/market-analysis/economic-calendar/"; 

                using (var stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

                var response = (HttpWebResponse)request.GetResponse(); 
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                AuthToken tokenModel = JsonConvert.DeserializeObject<AuthToken>(responseString);

                return string.Format("{1} {0}", tokenModel.Access_token, tokenModel.Token_type);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        //date format: 20180815
        //each time get one months data
        public string GetMainCalendar(string token, string month, string year)
        {
            var firstDayOfMonth = new DateTime(Int32.Parse(year), Int32.Parse(month), 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
            if (firstDayOfMonth > DateTime.Now) return string.Empty; 

            string startDate = firstDayOfMonth.ToString("yyyyMMdd");
            string endDate = lastDayOfMonth.ToString("yyyyMMdd"); ;

            if (string.IsNullOrEmpty(token)) return string.Empty;

            try
            { 
                var postData = string.Format("authorization={0}&callback={1}&timezone={2}&view=range&start={3}&end={4}&volatility=0&culture=en-gb&_=1535343047548",
                    token, "", "US+Eastern+Standard+Time", startDate, endDate);

                var request = (HttpWebRequest)WebRequest.Create("https://calendar.fxstreet.com/EventDateWidget/GetMain?" + postData);
                request.Method = "GET";
                request.ContentType = "application/javascript; charset=utf-8";
                request.Accept = "ation/json, text/javascript, */*; q=0.01"; 
                request.Headers["Origin"] = "https://www.forex.com";
                request.Referer = "https://www.forex.com/cn/market-analysis/economic-calendar/";   
                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                return responseString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// string html = File.ReadAllText("./sample/html-snippet.txt"); 
        /// </summary>
        /// <param name="html"></param>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public List<EconomicCalender> ParseHtml(string html,string month,string year)
        { 
            List<EconomicCalender> calendar = new List<EconomicCalender>();
            if (string.IsNullOrEmpty(html)) return calendar;
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            if (htmlDoc == null) return calendar;

            var htmlThead = htmlDoc.DocumentNode.SelectSingleNode("//table/thead");
            var htmlTBody = htmlDoc.DocumentNode.SelectSingleNode("//table/tbody");
            var currentElement = htmlTBody.FirstChild;

            string currentDate = string.Empty;
            string timeZone = htmlThead.SelectSingleNode("tr/th[1]").InnerText.Trim();


            while (currentElement != null)
            {
                if (currentElement.NodeType == HtmlNodeType.Element && currentElement.Attributes["class"] != null)
                {
                    //date range is one day.
                    //new date
                    if (currentElement.Attributes["class"].Value == "fxst-dateRow")
                    {
                        currentDate = currentElement.InnerText.Trim();
                    }

                    if(currentElement.Attributes["class"].Value.IndexOf("fxst-tr-event") >=0)
                    {
                        //add current element to array list
                        EconomicCalender entity = new EconomicCalender(); 
                        var secondChild = currentElement.SelectSingleNode("td[2]//b");
                        entity.TimeZone = timeZone;
                        entity.Year = year;
                        entity.Month = month;
                        entity.Day = currentDate.Substring(currentDate.LastIndexOf(" ") + 1);
                        entity.DateText = currentDate; 
                        entity.Time = currentElement.SelectSingleNode("td[1]").InnerText.Trim();
                        entity.CountryCode = secondChild.InnerText.Trim();
                        entity.CountryName = secondChild.Attributes["title"].Value;
                        entity.Event = currentElement.SelectSingleNode("td[3]//a").InnerText.Trim();
                        entity.Vol = currentElement.SelectSingleNode("td[4]//span").InnerText.Trim();
                        entity.Actual= currentElement.SelectSingleNode("td[5]").InnerText.Trim();
                        entity.Consensus = currentElement.SelectSingleNode("td[6]").InnerText.Trim();
                        entity.Previous = currentElement.SelectSingleNode("td[7]").InnerText.Trim();
                        calendar.Add(entity);
                    }
                } 

                //Console.WriteLine(currentElement.OuterHtml);

                currentElement = currentElement.NextSibling;
            } 

            return calendar; 
        } 

        public void InsertToDatabase(List<EconomicCalender> calendar)
        {
            if (calendar == null || calendar.Count == 0) return;

            string year = calendar[0].Year;
            string month = calendar[0].Month;
            using (var context = new ScrapingWebContext())
            {
                int count = context.EconomicCalenders.Count(p => p.Year == year && p.Month == month);

                if (count == 0)
                {
                    context.AddRange(calendar);
                    context.SaveChanges();
                }
            }
        }

        public void Run()
        {
            int year = DateTime.Now.Year;

            //year
            for(int i = year - 10; i <= year; i++)
            {
                //month
                for(int j = 1; j <= 12; j++)
                {
                    string token = this.GetAuthToken();
                    string html = this.GetMainCalendar(token, j.ToString(), i.ToString());
                    var elements = this.ParseHtml(html, j.ToString(), i.ToString());
                    this.InsertToDatabase(elements);

                    Console.WriteLine(string.Format("Completed for year {0}, month {1}", i, j));
                }

                Thread.Sleep(1000);
            }

            Console.WriteLine("Job done.");
        }
    }
}
