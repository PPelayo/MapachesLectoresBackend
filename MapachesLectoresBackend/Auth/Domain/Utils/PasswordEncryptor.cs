using MapachesLectoresBackend.Users.Domain.Model;
using Microsoft.AspNetCore.Identity;

namespace MapachesLectoresBackend.Auth.Domain.Utils;

public static class PasswordEncryptor
{
    private static readonly PasswordHasher<object> Hasher = new();

    public static string Encrypt(User user, string pwd)
    {
        return Hasher.HashPassword(user, pwd);
    }

    public static bool ValidatePassword(User user, string unencryptedPwd)
    {
        var resultado = Hasher.VerifyHashedPassword(user, user.Password, unencryptedPwd);
        return resultado == PasswordVerificationResult.Success;
    }
}