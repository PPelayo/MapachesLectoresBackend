namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public class PaginationResult<T>
{
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();
}