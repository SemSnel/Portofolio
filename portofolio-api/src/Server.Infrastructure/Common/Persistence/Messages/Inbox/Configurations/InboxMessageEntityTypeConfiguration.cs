using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Entities.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Inbox.Configurations;

/// <summary>
/// Entity type configuration for <see cref="InboxMessage"/>.
/// </summary>
public class InboxMessageEntityTypeConfiguration : IEntityTypeConfiguration<InboxMessage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<InboxMessage> builder)
    {
        builder
            .ToTable(
                "InboxMessages",
                "dbo");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .HasConversion<string>(
                id => id.ToString(),
                id => new InboxMessageId(Guid.Parse(id)))
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
                data => data)
                .IsRequired();

        builder
            .Property(b => b.ProcessedOn)
            .IsRequired(false);
    }
}