namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public class QueryPagination : IPagintaion
{
    public QueryPagination(IPagintaion pagintaion)
    {
        Limit = pagintaion.Limit + 1;
        Offset = pagintaion.Offset;
    }
    
    public int Limit { get; }
    public int Offset { get; }
    
}