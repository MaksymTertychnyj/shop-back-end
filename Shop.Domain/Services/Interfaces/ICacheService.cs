using Microsoft.Extensions.Caching.Memory;
using Shop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Services.Interfaces
{
    public interface ICacheService<TEntity>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetEntitiesAsync();
        Task UpdateEntitiesAsync();
    }
}
