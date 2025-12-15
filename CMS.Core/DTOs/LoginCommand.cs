using System;

namespace CMS.Core.DTOs;

public class LoginCommand
{
    

    public string Email {get;}
    public string Password {get;}

    public LoginCommand(string email, string password)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
            
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty", nameof(password));

        Email = email.Trim().ToLowerInvariant();
        Password = password;
    }
}
