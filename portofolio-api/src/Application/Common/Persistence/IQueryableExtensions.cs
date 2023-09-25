using Mapster;
using Microsoft.EntityFrameworkCore;

namespace SemSnel.Portofolio.Application.Common.Persistence;

public static class IQueryableExtensions
{
    /// <summary>
    /// Projects an IQueryable to a type using Mapster
    /// </summary>
    /// <param name="source">source IQueryable</param>
    /// <param name="mapper">Mapster IMapper</param>
    /// <typeparam name="TSource">Source type</typeparam>
    /// <typeparam name="TDestination">Destination type</typeparam>
    /// <returns>The projected IQueryable</returns>
    public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IMapper mapper)
    {
        return source.ProjectToType<TDestination>(mapper.Config);
    }
    
    /// <summary>
    /// Maps an IQueryable to a PaginatedList
    /// </summary>
    /// <param name="source"></param>
    /// <param name="pageIndex"></param>
    /// <param name="pageSize"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        return new PaginatedList<T>(items, count, pageIndex, pageSize);
    }
}

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