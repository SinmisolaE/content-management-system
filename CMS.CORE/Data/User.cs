using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.CORE.Data;

public class User
{
    [Key]
    public Guid Id {get; private set;}
    public string FirstName {get; private set;}
    public string LastName {get; private set;}
    public string Email {get; private set;}
    public string HashedPassword {get; private set;}
    public string PhotoBase64 {get; set;}


    public Guid RoleId;
    public UserRole Role;

    public User(string firstName, string lastName, string email, string hashedPassword, string photo, Guid roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        PhotoBase64 = photo;
        RoleId = roleId;
    }
}
