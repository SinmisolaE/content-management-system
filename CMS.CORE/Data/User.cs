using System;

namespace CMS.CORE.Data;

public class User
{
    public Guid Id {get; private set;}
    public string FirstName {get; private set;}
    public string LastName {get; private set;}
    public string Email {get; private set;}
    public string HashedPassword {get; private set;}
    public string PhotoBase64 {get; set;}


    public Guid RoleId;
    public UserRole Role;

    public User(Guid id, string firstName, string lastName, string email, string hashedPassword, string photo, Guid roleId)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        PhotoBase64 = photo;
        RoleId = roleId;
    }
}
