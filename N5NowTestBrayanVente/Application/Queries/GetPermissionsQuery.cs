using MediatR;
using N5NowTestBrayanVente.Application.DTOs;

namespace N5NowTestBrayanVente.Application.Queries
{
    public record GetPermissionsQuery(int Id) : IRequest<PermissionsResultDTO>;
}
