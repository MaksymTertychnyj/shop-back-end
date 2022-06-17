using MediatR;
using Shop.Domain.Dto.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure.MediatR.NovaPochta
{
    public class GetAreasRequest : IRequest<List<AreaDto>>
    {
    }
}
