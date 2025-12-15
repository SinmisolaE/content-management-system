using System;

namespace CMS.Core.DTOs;

public class AuthResult
{
    public string AccessToken {get;}
    public string RefreshToken {get;}

    public AuthResult(string accessToken, string refreshToken)
    {
        AccessToken = accessToken;
        RefreshToken = refreshToken;
    }

}
