using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Dto.NovaPochta
{
    public class ResponseDto<TEntity>
        where TEntity : class
    {
#pragma warning disable
        public List<TEntity> data { get; set; } = new List<TEntity>();

#pragma warning enable
    }
}
