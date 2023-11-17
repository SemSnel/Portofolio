using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts;
using SemSnel.Portofolio.Domain.Contexts.WeatherForecasts.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Contexts.WeatherForecasts.Persistence;

/// <summary>
/// Entity type configuration for <see cref="WeatherForecast"/>.
/// </summary>
public sealed class WeatherForecastsEntityTypeConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    /// <inheritdoc/>
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
            .IsRequired()
            .HasConversion(x => x.Value, x => new Temperature(x));

        builder.Property(x => x.Summary)
            .HasColumnName("Summary")
            .HasConversion<string?>(
                x => x!.Name,
                x => WeatherForecastSummary.Create(x!).Value)
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

        builder
            .HasQueryFilter(x => x.DeletedOn == null);
    }
}