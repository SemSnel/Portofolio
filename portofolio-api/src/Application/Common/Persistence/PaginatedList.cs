namespace SemSnel.Portofolio.Application.Common.Persistence;

public sealed class PaginatedList<T>
{
    public int Count { get; }
    public int PageIndex { get; }
    public int TotalPages { get; }
    
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;

    public IEnumerable<T> Items { get; }
    
    public PaginatedList(IEnumerable<T> items, int count, int pageIndex, int pageSize)
    {
        Count = count;
        PageIndex = pageIndex;
        TotalPages = (int) Math.Ceiling(count / (double) pageSize);
        Items = items;
    }
}