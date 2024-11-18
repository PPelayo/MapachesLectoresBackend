namespace MapachesLectoresBackend.Core.Domain.Model.Vo;


public record UuidVo : ValueObject<Guid>
{
    public UuidVo(string uuid) : base(Guid.Parse(uuid))
    {
    }
    
    public UuidVo() : this(Guid.NewGuid())
    {
    }
    
    public UuidVo(Guid guid) : this(guid.ToString())
    {
    }
}
