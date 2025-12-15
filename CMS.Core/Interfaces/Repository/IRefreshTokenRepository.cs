using System;
using CMS.Core.Data;

namespace CMS.Core.Interfaces.Repository;

public interface IRefreshTokenRepository
{
    Task<bool> AddRefreshTokenAsync(RefreshToken refreshToken);

    
}
