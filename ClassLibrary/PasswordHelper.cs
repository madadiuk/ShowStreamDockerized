using System;
using System.Security.Cryptography;
using System.Text;

public static class PasswordHelper
{
    public static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var saltedPassword = string.Format("{0}{1}", password, GetSalt());
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            return Convert.ToBase64String(bytes);
        }
    }

    private static string GetSalt()
    {
        // Generate a salt value here (e.g., from a configuration or environment variable)
        return "random_salt_value"; // Replace with a more secure and dynamic salt generation approach
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash)
    {
        var hashedPassword = HashPassword(enteredPassword);
        return hashedPassword == storedHash;
    }
}
