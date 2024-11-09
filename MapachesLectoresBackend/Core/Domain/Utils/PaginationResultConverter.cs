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
            Data = listCount > queryPagination.Limit ? list.Take(queryPagination.Limit - 1) : list,
            HasNext = listCount > queryPagination.Limit - 1,
            HasPrevious = queryPagination.Offset > 0,
            Limit = queryPagination.Limit - 1,
            Offset = queryPagination.Offset
        };
    }
}