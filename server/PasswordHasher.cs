using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

public class PasswordHasher
{
    private readonly string _secretKey;

    public PasswordHasher(IConfiguration configuration)
    {
        _secretKey = configuration["HashSecret"] 
                     ?? throw new ArgumentNullException("SecretKey is missing in appsettings.json");
    }

    /// <summary>
    /// Hashes the password using HMACSHA256 with the secret key.
    /// </summary>
    public string HashPassword(string password)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_secretKey));
        var hash = Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(password)));
        return hash;
    }

    /// <summary>
    /// Verifies if the input password matches the stored hash.
    /// </summary>
    public bool VerifyPassword(string password, string storedHash)
    {
        var hash = HashPassword(password);
        return hash == storedHash;
    }
}
