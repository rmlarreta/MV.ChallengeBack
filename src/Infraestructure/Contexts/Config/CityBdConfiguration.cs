using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WeatherRequest.Infraestructure.Entities.Bd;

namespace WeatherRequest.Infraestructure.Contexts.Config
{
    public class CityBdConfiguration : IEntityTypeConfiguration<CityBd>
    {
        public void Configure(EntityTypeBuilder<CityBd> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();
            builder.Property(c => c.Name).IsRequired();
            builder.Property(c => c.Pais).IsRequired(false);
            builder.Property(c => c.IdDouble).IsRequired(false);
        }
    }
}
