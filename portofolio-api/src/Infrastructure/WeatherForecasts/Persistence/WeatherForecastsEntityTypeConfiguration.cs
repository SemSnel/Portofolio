using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Domain.WeatherForecasts;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts;

public sealed class WeatherForecastsEntityTypeConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.ToTable("WeatherForecasts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("Id")
            .IsRequired();

        builder.Property(x => x.Date)
            .HasColumnName("Date")
            .IsRequired();

        builder.Property(x => x.TemperatureC)
            .HasColumnName("TemperatureC")
            .IsRequired();

        builder.Property(x => x.Summary)
            .HasColumnName("Summary")
            .IsRequired(false);

        builder.Property(x => x.CreatedBy)
            .HasColumnName("CreatedBy")
            .IsRequired(false);

        builder.Property(x => x.CreatedOn)
            .HasColumnName("CreatedOn")
            .IsRequired();

        builder.Property(x => x.LastModifiedBy)
            .HasColumnName("LastModifiedBy")
            .IsRequired(false);

        builder.Property(x => x.LastModifiedOn)
            .HasColumnName("LastModifiedOn")
            .IsRequired(false);

        builder.Property(x => x.DeletedBy)
            .HasColumnName("DeletedBy")
            .IsRequired(false);

        builder.Property(x => x.DeletedOn)
            .HasColumnName("DeletedOn")
            .IsRequired(false);

        builder.Property(x => x.IsDeleted)
            .HasColumnName("IsDeleted")
            .IsRequired();

        builder.HasQueryFilter(x => !x.IsDeleted);
    }
}