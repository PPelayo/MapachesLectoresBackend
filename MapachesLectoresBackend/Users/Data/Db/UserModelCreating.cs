using MapachesLectoresBackend.Core.Data.Db;
using MapachesLectoresBackend.Users.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace MapachesLectoresBackend.Users.Data.Db;

public class UserModelCreating
{
    public static void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyBaseEntityConfig<User>();
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("user");
            entity.HasKey(e => e.Id);
        });
    }
}