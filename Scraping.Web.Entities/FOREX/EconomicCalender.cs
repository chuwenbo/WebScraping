using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Scraping.Web.Entities.FOREX
{
    public class EconomicCalender
    {
        [Key]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Time { get; set; }
        [MaxLength(50)]
        public string TimeZone { get; set; }
        [MaxLength(50)]
        public string Country { get; set; }
        [MaxLength(200)]
        public string Event { get; set; }
        [MaxLength(50)]
        public string Vol { get; set; }
        [MaxLength(50)]
        public string Actual { get; set; }
        [MaxLength(50)]
        public string Consensus { get; set; }
        [MaxLength(50)]
        public string Previous { get; set; }
        public string Description { get; set; }
    }
}
