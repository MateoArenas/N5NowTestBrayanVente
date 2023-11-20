using MediatR;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Aggregates.UnitOfWorkAggregate.Interfaces;

namespace N5NowTestBrayanVente.Application.Handlers
{
    public class RequestPermissionHandler : IRequestHandler<RequestPermissionCommand, PermissionsResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;

        public RequestPermissionHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissionsResultDTO> Handle(RequestPermissionCommand request, CancellationToken cancellationToken)
        {
            Permissions permissions = new()
            {
                NombreEmpleado = request.permisionRequestDTO.NombreEmpleado,
                ApellidoEmpleado = request.permisionRequestDTO.ApellidoEmpleado,
                TipoPermiso = request.permisionRequestDTO.TipoPermiso,
                FechaPermiso = request.permisionRequestDTO.FechaPermiso,
            };

            Permissions permissionResult = await _unitOfWork.GeneralRepository<Permissions>().InsertAsync(permissions);
            _unitOfWork.Commit();

            return new PermissionsResultDTO
            {
                NombreEmpleado = permissionResult.NombreEmpleado,
                ApellidoEmpleado = permissions.ApellidoEmpleado,
                TipoPermiso = permissionResult.PermissionType.Descripcion,
                FechaPermiso = permissions.FechaPermiso,
            };
        }
    }
}
