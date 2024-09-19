using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Users.Application.DTOs;

public class CreateUserDto
{
    public required string Name { get; set; }
    public required DateTime DateOfBirth { get; set; }
    public required string CPF { get; set; }
    [PasswordPropertyText]
    public required string Password { get; set; }
    [EmailAddress]
    public required string Email { get; set; }
    public DateTime CreateAt { get; set; } = DateTime.Now;
    public DateTime UpdateAt { get; set; } = DateTime.Now;
}