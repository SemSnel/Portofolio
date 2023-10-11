using SemSnel.Portofolio.Application.Common.DateTime;

namespace SemSnel.Portofolio.Infrastructure.Common.DateTime;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime Now() => System.DateTime.Now;
}