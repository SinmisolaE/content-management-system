using System;

namespace CMS.Core.DTOs;

public class SignUpCommand
{
    public SignUpCommand(string firstName, string lastName, string email, string password, string role, string photoBase64)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new ArgumentException("First Name cannot be empty", nameof(firstName));

        if (string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Last Name cannot be empty", nameof(lastName));
        
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
            
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty", nameof(password));

        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty", nameof(role));



        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        Role = role;
        PhotoBase64 = photoBase64;
    }

    public string FirstName {get;}
    public string LastName {get;}
    public string Email {get;}
    public string Password {get;}
    public string Role {get;}
    public string PhotoBase64 {get;}

}
