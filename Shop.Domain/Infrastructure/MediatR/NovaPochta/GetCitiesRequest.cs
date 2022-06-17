using MediatR;
using Shop.Domain.Dto.NovaPochta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Domain.Infrastructure.MediatR.NovaPochta
{
    public class GetCitiesRequest : IRequest<List<CityDto>>
    {
        public string AreaRef { get; set; } = string.Empty;

        public GetCitiesRequest(string areaRef)
        {
            AreaRef = areaRef;
        }
    }
}
