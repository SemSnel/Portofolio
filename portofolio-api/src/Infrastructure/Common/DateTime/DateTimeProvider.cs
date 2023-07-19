namespace SemSnel.Portofolio.Application.Common.DateTime;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime Now() => System.DateTime.Now;
}