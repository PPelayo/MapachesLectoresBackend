using MapachesLectoresBackend.Core.Domain.Model.Vo;

namespace MapachesLectoresBackend.Reviews.Domain.Model.Vo;

public record RatingVo : ValueObject<uint>
{
    public RatingVo(uint value) : base(value)
    {
        if (value > 5)
        {
            throw new ArgumentException("El valor debe estar entre 0  y 5", nameof(RatingVo));
        }
    }    
}