using System;
using CMS.Core.DTOs;

namespace CMS.Core.Interfaces.Services;

public interface IAuthService
{
    public Task<AuthResult> LoginAsync(LoginCommand loginCommand);
    public Task<bool> Logout(Guid userId);
    public Task<AuthResult> SignUpAsync(SignUpCommand signUpCommand);
}
