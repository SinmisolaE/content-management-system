using System;
using System.ComponentModel;
using CMS.Core.Data;
using CMS.Core.DTOs;
using CMS.Core.Interfaces.Infrastructure;
using CMS.Core.Interfaces.Repository;
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
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;
    private readonly IRoleRepository _roleRepository;
    private readonly IRefreshTokenRepository _refreshToken;
    private readonly IUnitOfWork _unitOfWork;
    

    public AuthService(ITokenProvider tokenProvider, IPasswordHasher passwordHasher, IUserRepository userRepository,
        IRoleRepository roleRepository, IRefreshTokenRepository refreshToken, IUnitOfWork unitOfWork
    )
    {
        _tokenProvider = tokenProvider;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _refreshToken = refreshToken;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResult> LoginAsync(LoginCommand loginCommand)
    {

        User user = await _userRepository.GetUserByEmailAsync(loginCommand.Email);

        if (user == null)
        {
            throw new ArgumentNullException("User not found");
        }

        if (!_passwordHasher.VerifyPassword(loginCommand.Password, user.HashedPassword))
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

        var resp = await _refreshToken.AddRefreshTokenAsync(refreshToken);
        if (!resp) throw new Exception("Error in signing in!");

        await _unitOfWork.SaveEntitiesAsync();


        return new AuthResult(accessToken, refreshToken.Token);

    }

    /*
        Deletes all refresh tokens for user in db
    */
    public async Task<bool> Logout(Guid userId)
    {
        try
        {
            
            await _refreshToken.DeleteRefreshTokenAsync(userId);
            return true;
        } catch (Exception) {
            return false;
        }
    }

    public async Task<AuthResult> SignUpAsync(SignUpCommand signUpCommand)
    {
        // Confirm if email exists
        var user = await _userRepository.GetUserByEmailAsync(signUpCommand.Email);

        if (user != null) throw new ApplicationException("Email already exists");

        // Hash user password
        string hashedPassword = _passwordHasher.HashPassword(signUpCommand.Password);

        // Get Role of User
        UserRole role = await _roleRepository.GetRoleByName(signUpCommand.Role);
        if (role == null) throw new ApplicationException("Role doesnt exist");


        // Create user
        User user1 = new User(
            signUpCommand.FirstName,
            signUpCommand.LastName,
            signUpCommand.Email,
            hashedPassword,
            signUpCommand.PhotoBase64,
            role.Id
        );

        var newUser = await _userRepository.AddUserAsync(user1);

        string accessToken = _tokenProvider.GenerateJwt(newUser);

        string refreshTokenString = _tokenProvider.GenerateRefreshToken();

        var refreshToken = new RefreshToken(
            newUser.Id,
            refreshTokenString,
            DateTime.UtcNow.AddDays(7)
        );

        var resp = await _refreshToken.AddRefreshTokenAsync(refreshToken);
        if (!resp) throw new Exception("Error in signing in!");

        await _unitOfWork.SaveEntitiesAsync();

        return new AuthResult(accessToken, refreshToken.Token);

    }

    /*
        Upon access token expires
        Refresh token regenerates access tokens for auth
    */
    public async Task<AuthResult> RefreshTokenLogIn(string refreshToken)
    {
        if (refreshToken == null || string.IsNullOrEmpty(refreshToken))
        {
            throw new Exception("Error authenticating!");
        }

        var findRefreshToken = await _refreshToken.GetRefreshTokenAsync(refreshToken);
        if (findRefreshToken == null || findRefreshToken.Expires < DateTime.UtcNow){
             throw new Exception("Refresh token has expired");
        }

        string accessToken = _tokenProvider.GenerateJwt(findRefreshToken.User);

        // Rotate refresh token
        findRefreshToken.Token = _tokenProvider.GenerateRefreshToken();
        findRefreshToken.Expires = DateTime.UtcNow.AddDays(7);

        await _unitOfWork.SaveEntitiesAsync();
        

        return new AuthResult(accessToken, findRefreshToken.Token);
    }
}
