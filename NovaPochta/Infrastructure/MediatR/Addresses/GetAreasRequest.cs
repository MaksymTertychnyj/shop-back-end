using MediatR;
using NovaPochta.Dto;

namespace NovaPochta.Infrastructure.MediatR.Addresses
{
    public class GetAreasRequest : IRequest<List<AreaDto>>
    {
    }
}
