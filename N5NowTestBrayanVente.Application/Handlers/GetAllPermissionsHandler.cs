using MediatR;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Queries;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Aggregates.UnitOfWorkAggregate.Interfaces;

namespace N5NowTestBrayanVente.Application.Handlers
{
    public class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery, IList<PermissionsResultDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllPermissionsHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IList<PermissionsResultDTO>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            List<Permissions> permissions = await _unitOfWork.GeneralRepository<Permissions>().GetAllAsync();

            return permissions.Select(x => new PermissionsResultDTO
            {
                NombreEmpleado = x.NombreEmpleado,
                ApellidoEmpleado = x.ApellidoEmpleado,
                TipoPermiso = x.PermissionType.Descripcion,
                FechaPermiso = x.FechaPermiso
            }).ToList() ;
        }
    }
}
