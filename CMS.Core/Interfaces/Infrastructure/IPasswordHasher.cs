using System;

namespace CMS.Core.Interfaces.Infrastructure;

// Abstraction for password hash methid
public interface IPasswordHasher
{
    string HashPassword(string password);

    bool VerifyPassword(string password, string hashedPassword);
}
