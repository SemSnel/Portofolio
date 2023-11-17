namespace SemSnel.Portofolio.Domain._Common.Pagination;

/// <summary>
/// Extensions for <see cref="PaginatedList{T}"/>.
/// </summary>
public static class PaginatedListExtensions
{
    /// <summary>
    /// Maps a paginated list.
    /// </summary>
    /// <param name="source"> The source. </param>
    /// <param name="map"> The map. </param>
    /// <typeparam name="TSource"> The source type. </typeparam>
    /// <typeparam name="TDestination"> The destination type. </typeparam>
    /// <returns> The paginated list. </returns>
    public static PaginatedList<TDestination> Map<TSource, TDestination>(
        this PaginatedList<TSource> source,
        Func<TSource, TDestination> map)
    {
        return PaginatedList<TDestination>.Create(
            source.Items.Select(map),
            source.Count,
            source.PageIndex,
            source.TotalPages);
    }

    /// <summary>
    /// Maps a paginated list.
    /// </summary>
    /// <param name="source"> The source. </param>
    /// <param name="pageIndex"> The page index. </param>
    /// <param name="pageSize"> The page size. </param>
    /// <typeparam name="T"> The item type. </typeparam>
    /// <returns> The paginated list. </returns>
    public static PaginatedList<T> ToPaginatedList<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
    {
        var count = source.Count();

        var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

        return PaginatedList<T>.Create(items, count, pageIndex, pageSize);
    }
}