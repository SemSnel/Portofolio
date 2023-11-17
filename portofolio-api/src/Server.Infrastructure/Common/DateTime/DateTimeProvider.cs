using SemSnel.Portofolio.Server.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.DateTime;

/// <summary>
/// A class that provides the date time.
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    /// <inheritdoc/>
    public System.DateTime Now() => System.DateTime.Now;
}