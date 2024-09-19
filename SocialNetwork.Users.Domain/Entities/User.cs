using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.Users.Domain.Entities;

public class User
{
    [Key]
    [Required]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public required string Name { get; set; }
    [Required]
    public required DateTime DateOfBirth { get; set; }
    [Required]
    [StringLength(11)]
    public required string CPF { get; set; }
    [Required]
    [PasswordPropertyText]
    public required string Password { get; set; }
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    public DateTime CreateAt { get; set; }
    public DateTime UpdateAt { get; set; }
}