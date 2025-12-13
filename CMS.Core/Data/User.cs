using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Data;

public class User
{
    [Key]
    public Guid Id {get; private set;}

    [Required]
    [MaxLength(100)]
    public string FirstName {get; private set;}

    [Required]
    [MaxLength(100)]
    public string LastName {get; private set;}

    [Required]
    [MaxLength(255)]
    [EmailAddress]
    public string Email {get; private set;}

    [Required]
    [MaxLength(255)]
    public string HashedPassword {get; private set;}
    public string PhotoBase64 {get; set;}

    [Required]
    public Guid RoleId {get; private set;}
    public UserRole Role { get; private set; }

    public User(string firstName, string lastName, string email, string hashedPassword, string photoBase64, Guid roleId)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        HashedPassword = hashedPassword;
        PhotoBase64 = photoBase64;
        RoleId = roleId;
    }
}
