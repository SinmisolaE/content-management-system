using System;
using CMS.Core.Data;

namespace CMS.Core.Interfaces.Infrastructure;

public interface ITokenProvider
{
    string GenerateJwt(User user);
    string GenerateRefreshToken();
}
