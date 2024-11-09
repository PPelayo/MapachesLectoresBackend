using MapachesLectoresBackend.Core.Domain.Model.Pagination;

namespace MapachesLectoresBackend.Core.Domain.Utils;

public static class PaginationResultConverter
{
    public static PaginationResult<T> ToPaginationResult<T>(this IEnumerable<T> list, QueryPagination queryPagination)
    {
        list = list.ToArray();
        var listCount = list.Count();
        return new PaginationResult<T>()
        {
            Data = list.Take(listCount - 1),
            HasNext = listCount > queryPagination.Limit,
            HasPrevious = queryPagination.Offset > 0,
            Limit = queryPagination.Limit,
            Offset = queryPagination.Offset
        };
    }
}