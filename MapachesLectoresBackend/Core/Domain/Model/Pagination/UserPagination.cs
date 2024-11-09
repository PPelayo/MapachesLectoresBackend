namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public class UserPagination : IPagintaion
{
    private const int MaxLimit = 1000;
    
    private int _limit = MaxLimit;
    public int Limit
    {
        get => _limit;
        set => _limit = value > MaxLimit ? MaxLimit : value;
    }

    public int Offset { get; set; } = 0;
}