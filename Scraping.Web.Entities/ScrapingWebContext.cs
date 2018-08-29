//using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Scraping.Web.Entities.FOREX; 
using System.Collections.Generic; 

namespace Scraping.Web.Entities
{
    public class ScrapingWebContext : DbContext
    {
        
        public DbSet<EconomicCalender> EconomicCalenders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.;Database=ScrapingWeb;Trusted_Connection=True;");

         
        }
    }
}
