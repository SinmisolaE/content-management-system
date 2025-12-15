using System;
using CMS.Core.Data;

namespace CMS.Core.Interfaces.Repository;

public interface IUserRepository
{
    Task<User> GetUserByEmailAsync(string email);
    Task<ICollection<User>> GetUsersAsync();
    Task<User> GetUserByIdAsync(Guid guid);
    Task<ICollection<User>> GetUsersByRoleAsync(UserRole userRole);

    Task<User> AddUserAsync(User user);

    Task SaveChangesAsync();

}
