using System;

namespace CMS.Core.Data;

public class RefreshToken
{
    public Guid Id {get; private set;}
    public Guid UserId {get; private set;}
    public string Token {get; private set;}
    public DateTime Expires {get; private set;}

    public User user {get; private set;}
}
