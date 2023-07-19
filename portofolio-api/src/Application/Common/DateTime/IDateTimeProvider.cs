namespace SemSnel.Portofolio.Application.Common.DateTime;

public interface IDateTimeProvider
{
    public System.DateTime Now() => System.DateTime.Now;
}