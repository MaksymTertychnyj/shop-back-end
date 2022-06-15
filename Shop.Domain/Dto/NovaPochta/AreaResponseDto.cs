using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class AreaResponseDto<TEntity>
        where TEntity : class
    {
        public List<TEntity> data { get; set; } = new List<TEntity>();
    }
}
