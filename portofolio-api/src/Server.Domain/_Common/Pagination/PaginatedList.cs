namespace SemSnel.Portofolio.Domain._Common.Pagination;

public readonly record struct PaginatedList<T>(
    IEnumerable<T> Items,
    int Count,
    int PageIndex,
    int TotalPages)
{
    /// <summary>
    /// Gets a value indicating whether there is a previous page.
    /// </summary>
    public bool HasPreviousPage => PageIndex > 1;

    /// <summary>
    /// Gets a value indicating whether there is a next page.
    /// </summary>
    public bool HasNextPage => PageIndex < TotalPages;

    /// <summary>
    /// Creates a paginated list.
    /// </summary>
    /// <param name="items"> The items. </param>
    /// <param name="count"> The count. </param>
    /// <param name="pageIndex"> The page index. </param>
    /// <param name="pageSize"> The page size. </param>
    /// <returns> The paginated list. </returns>
    public static PaginatedList<T> Create(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        return new (items, count, pageIndex, (int)Math.Ceiling(count / (double)pageSize));
    }

    /// <summary>
    /// Creates an empty paginated list.
    /// </summary>
    /// <param name="pageIndex"> The page index. </param>
    /// <param name="pageSize"> The page size. </param>
    /// <returns> The empty paginated list. </returns>
    public static PaginatedList<T> Empty(int pageIndex, int pageSize)
    {
        return new (Enumerable.Empty<T>(), 0, pageIndex, 0);
    }
}