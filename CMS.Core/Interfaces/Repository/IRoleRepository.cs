using System;
using CMS.Core.Data;

namespace CMS.Core.Interfaces.Repository;

public interface IRoleRepository
{
    Task<UserRole> GetRoleByIdAsync(Guid guid);
    Task<UserRole> GetRoleByName(string name);
}
