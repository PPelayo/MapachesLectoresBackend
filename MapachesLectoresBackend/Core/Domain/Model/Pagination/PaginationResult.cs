namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public class PaginationResult<T>
{
    public bool HasNext { get; set; }
    public bool HasPrevious { get; set; }
    public int Offset { get; set; }
    public int Limit { get; set; }
    public IEnumerable<T> Data { get; set; } = new List<T>();
    
    public PaginationResult<TResult> Map<TResult>(Func<T, TResult> map)
    {
        return new PaginationResult<TResult>
        {
            HasNext = HasNext,
            HasPrevious = HasPrevious,
            Offset = Offset,
            Limit = Limit,
            Data = Data.Select(map)
        };
    }
}