using System;
using CMS.Core.Interfaces.Infrastructure;
using CMS.Infrastructure.Model;
using CMS.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CMS.Infrastructure;

public static class DependencyInjection
{
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionstring = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<AppDbContext>(o =>
            o.UseNpgsql(connectionstring)
        );

        services.AddSingleton<ITokenProvider, TokenProvider>();
        //services.AddSingleton<IPasswordHasher, PasswordHasher>();

        return services;
    }
}
