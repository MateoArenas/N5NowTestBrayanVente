using MediatR;
using N5NowTestBrayanVente.Application.DTOs;

namespace N5NowTestBrayanVente.Application.Commands
{
    public record ModifyPermissionCommand(int Id, PermisionRequestDTO permisionRequestDTO) : IRequest<PermissionsResultDTO>;
}
