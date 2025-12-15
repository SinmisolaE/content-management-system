using System;
using System.Runtime.CompilerServices;
using CMS.Core.Interfaces.Infrastructure;

namespace CMS.Infrastructure.Security;

/*
    Password Hasher
    Hashes with BCrypt with salt of 12
*/
public class PasswordHasher : IPasswordHasher
{
    private readonly int _workFactor = 12;

    // Hash provided password
    public string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password)) throw new ArgumentException("Password not provided");

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, _workFactor);

        return hashedPassword;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(hashedPassword))
            throw new ArgumentException("Password not provided");

        try
        {
            
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        } catch (Exception)
        {
            return false;
        }
    }
}
