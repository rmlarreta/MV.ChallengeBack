using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherRequest.Infraestructure.Entities.Bd;

namespace WeatherRequest.Infraestructure.Contexts.Config
{
    public class WeatherReportBdConfiguration : IEntityTypeConfiguration<WeatherReportBd>
    {
        public void Configure(EntityTypeBuilder<WeatherReportBd> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Id).ValueGeneratedOnAdd();
            builder.Property(w => w.CityId).IsRequired();
            builder.HasOne(w => w.CityBd)
                .WithMany()
                .HasForeignKey(w => w.CityId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.Property(w => w.Temperatura).IsRequired(false);
            builder.Property(w => w.Termica).IsRequired(false);
            builder.Property(w => w.Validez).IsRequired();
        }
    }
}
