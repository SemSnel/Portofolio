using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Infrastructure.Common.MessageBrokers.Persistence.Entities;

namespace SemSnel.Portofolio.Infrastructure.Common.OutBoxMessages.Persistence.Configurations;

public class OutBoxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutBoxMessage>
{
    public void Configure(EntityTypeBuilder<OutBoxMessage> builder)
    {
        builder
            .ToTable(
                "OutboxMessages", 
                "dbo");
        
        builder
            .HasKey(b => b.Id);
        
        builder
            .Property(b => b.Id)
            .ValueGeneratedNever();
        
        builder
            .Property(b => b.Type)
            .HasMaxLength(200)
            .IsRequired();
        
        builder
            .Property(b => b.Content)
            .IsRequired();
        
        builder
            .Property(b => b.CreatedOn)
            .IsRequired();
        
        builder
            .Property(b => b.ProcessedOn)
            .IsRequired(false);
    }
}