using SemSnel.Portofolio.Domain._Common.Monads.ErrorOr;

namespace SemSnel.Portofolio.Server.Application.Common.Files;

/// <summary>
/// A service for exporting data to csv.
/// </summary>
public interface ICsvService
{
    /// <summary>
    /// Exports the data to csv.
    /// </summary>
    /// <typeparam name="TData"> The data type. </typeparam>
    /// <param name="data"> The data. </param>
    /// <param name="mappers"> The mappers. </param>
    /// <param name="sheetName"> The sheet name. </param>
    /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
    Task<ErrorOr<FileDto>> Export<TData>(
        IEnumerable<TData> data,
        Dictionary<string, Func<TData, object>> mappers,
        string sheetName = "Sheet1");
}