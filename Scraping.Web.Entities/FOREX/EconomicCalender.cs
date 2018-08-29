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
        public string DateText { get; set; }
        [MaxLength(50)]
        public string Day { get; set; }
        [MaxLength(50)]
        public string Month { get; set; }
        [MaxLength(50)]
        public string Year { get; set; }
        [MaxLength(50)]
        public string TimeZone { get; set; }
        [MaxLength(50)]
        public string CountryCode { get; set; }
        [MaxLength(50)]
        public string CountryName { get; set; }
        [MaxLength(200)]
        public string Event { get; set; }
        /// <summary>
        /// volatility
        /// </summary>
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
