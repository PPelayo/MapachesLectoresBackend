namespace MapachesLectoresBackend.Core.Domain.Model.Vo;


public record UuidVo : ValueObject<string>
{
    public UuidVo(string uuid) : base(uuid)
    {
    }
    
    public UuidVo() : this(Guid.NewGuid())
    {
    }
    
    public UuidVo(Guid guid) : this(guid.ToString())
    {
    }
}
