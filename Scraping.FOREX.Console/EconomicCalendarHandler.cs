using Newtonsoft.Json;
using Scraping.FOREX.Application.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

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
        public string GetMainCalendar(string token,string startDate, string endDate)
        {
            if (string.IsNullOrEmpty(token)) return string.Empty;

            try
            { 
                var postData = string.Format("authorization={0}&callback={1}&timezone={2}&view=range&start={3}&end={4}&volatility=0&culture=en-gb&_=1535343047548",
                    token, "jQuery21109502571146360759_1535343047530", "US+Eastern+Standard+Time", startDate, endDate);

                var request = (HttpWebRequest)WebRequest.Create("https://calendar.fxstreet.com/EventDateWidget/GetMain?" + System.Web.HttpUtility.UrlEncode(postData));
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
    }
}
