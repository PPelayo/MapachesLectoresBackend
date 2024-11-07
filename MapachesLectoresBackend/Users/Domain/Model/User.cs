using System.ComponentModel.DataAnnotations;
using MapachesLectoresBackend.Core.Domain.Model;

namespace MapachesLectoresBackend.Users.Domain.Model;

public class User : BaseEntity
{
    public uint Id { get; set; }
    [MaxLength(255)]
    public required string Name { get; set; }
    [MaxLength(255)]
    public required string UserName { get; set; }
    [MaxLength(99999)]
    public string Password { get; set; } = null!;
    public uint Role { get; set; } = 0;
}