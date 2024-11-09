namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public static class PaginationExtensions
{
    public static QueryPagination ToQueryPagination(this IPagintaion pagintaion)
    {
        return new QueryPagination(pagintaion);
    }
}