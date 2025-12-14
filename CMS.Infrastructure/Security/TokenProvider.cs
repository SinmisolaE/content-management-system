using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CMS.Core.Data;
using CMS.Core.Interfaces.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CMS.Infrastructure.Security;

/*
    Authentication using Jwt

    Variables to set in jwt generation
*/
public class TokenProvider : ITokenProvider
{
    private readonly IConfiguration _configuration;

    private readonly string _issuerKey;
    private readonly string _audience;
    private readonly string _issuer;
    private readonly int _expirationToken;

    public TokenProvider(IConfiguration configuration)
    {
        _configuration = configuration;
        _issuerKey = _configuration["Jwt:Secret"];
        _audience = _configuration["Jwt:Audience"];
        _issuer = _configuration["Jwt:Issuer"];
        _expirationToken = _configuration.GetValue<int>("Jwt:ExpirationToken");
    }

    public string GenerateJwt(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_issuerKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            ]),

            SigningCredentials = credentials,
            Expires = DateTime.UtcNow.AddHours(_expirationToken),
            Issuer = _issuer,
            Audience = _audience
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);

        return jwt;
    }

    public string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
