using System;
using System.Collections.Generic;
using System.Text;

namespace Scraping.FOREX.Application.Model
{
    public class AuthToken
    {
        public string Access_token { get; set; }
        public string Token_type { get; set; }
        public string expires_in { get; set; }
    }
}
