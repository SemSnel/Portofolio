using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities;
using SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Entities.ValueObjects;

namespace SemSnel.Portofolio.Infrastructure.Common.Persistence.Messages.Domain.Configurations;

/// <summary>
/// Entity type configuration for <see cref="DomainMessage"/>.
/// </summary>
public class DomainMessageEntityTypeConfiguration : IEntityTypeConfiguration<DomainMessage>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<DomainMessage> builder)
    {
        builder
            .ToTable(
                "DomainMessages",
                "dbo");

        builder
            .HasKey(b => b.Id);

        builder
            .Property(b => b.Id)
            .HasConversion<string>(
                id => id.Value.ToString(),
                id => new DomainMessageId(Guid.Parse(id)))
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
            .Property(x => x.CreatedOn)
            .IsRequired();

        builder
            .Property(b => b.ProcessedOn)
            .IsRequired(false);
    }
}