using Mapster;
using Microsoft.EntityFrameworkCore;
using SemSnel.Portofolio.Domain._Common.Pagination;

namespace SemSnel.Portofolio.Server.Application.Common.Persistence;

/// <summary>
/// Extension methods for IQueryable.
/// </summary>
public static class IQueryableExtensions
{
    /// <summary>
    /// Projects an IQueryable to a type using Mapster.
    /// </summary>
    /// <param name="source">source IQueryable.</param>
    /// <param name="mapper">Mapster IMapper.</param>
    /// <typeparam name="TDestination">Destination type.</typeparam>
    /// <returns>The projected IQueryable.</returns>
    public static IQueryable<TDestination> ProjectTo<TDestination>(this IQueryable source, IMapper mapper)
    {
        return source.ProjectToType<TDestination>(mapper.Config);
    }

    /// <summary>
    /// Maps an IQueryable to a type using Mapster.
    /// </summary>
    /// <param name="source">source IQueryable.</param>
    /// <param name="pageIndex"> The page index. </param>
    /// <param name="pageSize"> The page size. </param>
    /// <typeparam name="T">Destination type.</typeparam>
    /// <returns>The mapped IQueryable.</returns>
    public static async Task<PaginatedList<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageIndex, int pageSize)
    {
        var count = await source.CountAsync();

        var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

        return PaginatedList<T>.Create(items, count, pageIndex, pageSize);
    }
}