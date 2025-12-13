using System;
using System.ComponentModel.DataAnnotations;

namespace CMS.CORE.Data;

public class UserRole
{
    public UserRole(string name, List<string> permissions)
    {
        Name = name;
        Permissions = permissions;
    }

    [Key]
    public Guid Id {get; private set;}
    public string Name {get; private set;}
    public List<string> Permissions {get; private set;}
}
