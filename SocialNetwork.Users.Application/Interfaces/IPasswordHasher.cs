namespace SocialNetwork.Users.Application.Interfaces;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string oldPassword, string hashedOldPassword);
}