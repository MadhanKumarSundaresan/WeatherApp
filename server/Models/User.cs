using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Banyan.Test.WeatherAPI
{
    public class User
{
    [Key]
    public int UserId { get; set; }  // Primary Key

    [Required]
    [MaxLength(100)]
    public string Username { get; set; } = string.Empty;

    [MaxLength(255)]
    public string PasswordHash { get; set; } = string.Empty;  // Store hashed password

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
    [NotMapped]
    [Required]
    public string Password { get; set; } = string.Empty;
}
}