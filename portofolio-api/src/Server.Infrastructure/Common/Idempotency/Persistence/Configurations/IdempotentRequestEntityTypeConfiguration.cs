using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Infrastructure.Common.Idempotency.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.Idempotency.Persistence.Configurations;

public class IdempotentRequestEntityTypeConfiguration : IEntityTypeConfiguration<IdempotentRequest>
{
    public void Configure(EntityTypeBuilder<IdempotentRequest> builder)
    {
        builder
            .ToTable(
                "IdempotentRequests", 
                "dbo");

        builder
            .HasKey(b => b.Id);
        
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
        
        builder
            .Property(b => b.Name)
            .HasMaxLength(200)
            .IsRequired();
        
        builder
            .Property(b => b.CreatedOn)
            .IsRequired();

        builder
            .Property(b => b.RequestId)
            .IsRequired();
        
        builder
            .Property(b => b.RequestBody)
            .IsRequired();
        
        builder
            .Property(b => b.RequestHeaders)
            .IsRequired();
        
        builder
            .Property(b => b.ResponseBody);

        builder
            .Property(b => b.ResponseStatusCode)
            .IsRequired();
    }
}