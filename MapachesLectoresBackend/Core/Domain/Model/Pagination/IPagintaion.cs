namespace MapachesLectoresBackend.Core.Domain.Model.Pagination;

public interface IPagintaion
{
    public int Limit { get; }
    public int Offset { get; }
}