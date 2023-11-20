using MediatR;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Queries;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Aggregates.UnitOfWorkAggregate.Interfaces;

namespace N5NowTestBrayanVente.Application.Handlers
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, PermissionsResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        public GetPermissionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<PermissionsResultDTO> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            Permissions permission = await _unitOfWork.GeneralRepository<Permissions>().GetAsync(request.Id);

            return new PermissionsResultDTO
            {
                NombreEmpleado = permission.NombreEmpleado,
                ApellidoEmpleado = permission.ApellidoEmpleado,
                TipoPermiso = permission.PermissionType.Descripcion,
                FechaPermiso = permission.FechaPermiso,
            };
        }
    }
}
