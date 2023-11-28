using Microsoft.EntityFrameworkCore;
using WeatherRequest.Infraestructure.Contexts.Config;
using WeatherRequest.Infraestructure.Entities.Bd;

namespace WeatherRequest.Infraestructure.Contexts
{
    public partial class WrContext : DbContext
    {
        public WrContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<CityBd> Cities { get; set; }
        public virtual DbSet<WeatherReportBd> WeatherReportBds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 

            modelBuilder.ApplyConfiguration(new CityBdConfiguration());
            modelBuilder.ApplyConfiguration(new WeatherReportBdConfiguration());
        }

    }
}
