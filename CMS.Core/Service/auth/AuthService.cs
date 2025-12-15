using System;
using System.ComponentModel;
using CMS.Core.Data;
using CMS.Core.DTOs;
using CMS.Core.Interfaces.Infrastructure;
using CMS.Core.Interfaces.Services;

namespace CMS.Core.Service.auth;


/*
    Auth service: handles user login and logout
    Ensures security with jwt tokens
    Ease of user experience with refresh tokens
*/
public class AuthService : IAuthService
{

    private readonly ITokenProvider _tokenProvider;

    public AuthService(ITokenProvider tokenProvider)
    {
        _tokenProvider = tokenProvider;
    }

    public async Task<AuthResult> LoginAsync(LoginCommand loginCommand)
    {

        User user = _userRepository.GetUserByEmail(loginCommand.Email);

        if (user == null)
        {
            throw new ArgumentNullException("User not found");
        }

        if (!_passwordHasher.Verify(loginCommand.Password, user.HashedPassword))
        {
            throw new ApplicationException("Username or password is incorrect");
        }

        string accessToken = _tokenProvider.GenerateJwt(user);

        string refreshTokenString = _tokenProvider.GenerateRefreshToken();

        var refreshToken = new RefreshToken(
            user.Id,
            refreshTokenString,
            DateTime.UtcNow.AddDays(7)
        );

        // save to db

        return new AuthResult(accessToken, refreshToken.Token);

    }

    public Task<bool> Logout(string refreshToken)
    {
        throw new NotImplementedException();
    }

    public Task<AuthResult> SignUpAsync(SignUpCommand signUpCommand)
    {
        throw new NotImplementedException();
    }
}
