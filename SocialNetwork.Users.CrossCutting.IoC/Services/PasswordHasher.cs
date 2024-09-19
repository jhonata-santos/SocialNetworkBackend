using SocialNetwork.Users.Application.Interfaces;

namespace SocialNetwork.Users.CrossCutting.IoC.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string plainPassword)
    {
        return BCrypt.Net.BCrypt.Verify(hashedPassword, plainPassword);
    }
}