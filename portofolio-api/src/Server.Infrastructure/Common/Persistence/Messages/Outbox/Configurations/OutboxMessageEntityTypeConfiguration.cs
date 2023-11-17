using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Outbox.Configurations;

/// <summary>
/// Entity type configuration for <see cref="OutboxMessage"/>.
/// </summary>
public class OutboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder
            .ToTable(
                "OutboxMessages",
                "dbo");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .HasConversion<string>(
                id => id.ToString(),
                id => new OutboxMessageId(Guid.Parse(id)))
            .IsRequired();

        builder
            .Property(b => b.Type)
            .HasConversion<string>(
                data => data,
                data => data);

        builder
            .Property(b => b.Data)
            .HasConversion<string>(
                data => data,
                data => data);

        builder
            .Property(b => b.ProcessedOn)
            .IsRequired(false);
    }
}