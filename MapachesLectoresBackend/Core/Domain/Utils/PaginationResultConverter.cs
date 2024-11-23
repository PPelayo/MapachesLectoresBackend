using MapachesLectoresBackend.Core.Domain.Model.Pagination;

namespace MapachesLectoresBackend.Core.Domain.Utils;

public static class PaginationResultConverter
{
    public static PaginationResult<T> ToPaginationResult<T>(this IEnumerable<T> list, QueryPagination queryPagination)
    {
        list = list.ToArray();
        var listCount = list.Count();

        var data = listCount > queryPagination.Limit - 1 ? list.Take(queryPagination.Limit - 1).ToList() : list.ToList();
        return new PaginationResult<T>()
        {
            Data = data,
            HasNext = listCount > queryPagination.Limit - 1,
            HasPrevious = queryPagination.Offset > 0,
            Limit = queryPagination.Limit - 1,
            Offset = queryPagination.Offset,
            Count = data.Count
        };
    }
}