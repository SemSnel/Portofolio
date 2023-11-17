namespace SemSnel.Portofolio.Server.Application.Common.DateTime;

/// <summary>
/// A service to get the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    /// <summary>
    /// Returns the current date and time.
    /// </summary>
    /// <returns> The current date and time. </returns>
    public System.DateTime Now() => System.DateTime.Now;
}