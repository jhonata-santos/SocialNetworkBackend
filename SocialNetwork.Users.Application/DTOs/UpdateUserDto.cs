using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Users.Application.DTOs;

public class UpdateUserDto
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string CPF { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    [PasswordPropertyText]
    public required string Password { get; set; }
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}