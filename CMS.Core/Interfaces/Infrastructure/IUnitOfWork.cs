using System;

namespace CMS.Core.Interfaces.Infrastructure;

public interface IUnitOfWork
{
    Task SaveEntitiesAsync();
}
