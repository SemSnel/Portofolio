using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Domain.Orders;

namespace SemSnel.Portofolio.Infrastructure.WeatherForecasts.Orders;

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders");
        
        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(e => e.CreatedBy);
        
        builder.Property(e => e.CreatedOn)
            .IsRequired();
        
        builder.Property(e => e.LastModifiedBy);
    }
}