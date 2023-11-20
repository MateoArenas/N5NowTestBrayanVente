using MediatR;
using N5NowTestBrayanVente.Application.DTOs;

namespace N5NowTestBrayanVente.Application.Commands
{
    public record RequestPermissionCommand(PermisionRequestDTO permisionRequestDTO) : IRequest<PermissionsResultDTO>;
}
