using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.Core.Data;

public class RefreshToken
{
    public RefreshToken(Guid userId, string token, DateTime expires)
    {
        UserId = userId;
        Token = token;
        Expires = expires;
    }

    [Key]
    public Guid Id {get; private set;}
    public Guid UserId {get; private set;}
    public string Token {get; set;}
    public DateTime Expires {get; set;}

    public User User {get; private set;}
}
