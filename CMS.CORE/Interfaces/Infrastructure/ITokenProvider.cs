using System;
using CMS.CORE.Data;

namespace CMS.CORE.Interfaces.Infrastructure;

public interface ITokenProvider
{
    string GenerateJwt(User user);
}
