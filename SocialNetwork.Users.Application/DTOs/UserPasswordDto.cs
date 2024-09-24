using System.ComponentModel;

namespace SocialNetwork.Users.Application.DTOs;

public class UserPasswordDto
{
    public int Id { get; set; }
    [PasswordPropertyText]
    public required string OldPassword { get; set; }
    [PasswordPropertyText]
    public required string NewPassword { get; set; }
}