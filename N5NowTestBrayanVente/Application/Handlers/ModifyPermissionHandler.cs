using MediatR;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Enums;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;

namespace N5NowTestBrayanVente.Application.Handlers
{
    public class ModifyPermissionHandler : IRequestHandler<ModifyPermissionCommand, PermissionsResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducer _kafkaProducer;
        public ModifyPermissionHandler(IUnitOfWork unitOfWork, IKafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<PermissionsResultDTO> Handle(ModifyPermissionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaProducer.ProduceKafkaMessage(KafkaMessageEnum.Modify);

                Permissions permissions = new()
                {
                    Id = request.Id,
                    NombreEmpleado = request.permisionRequestDTO.NombreEmpleado,
                    ApellidoEmpleado = request.permisionRequestDTO.ApellidoEmpleado,
                    TipoPermiso = request.permisionRequestDTO.TipoPermiso,
                    FechaPermiso = request.permisionRequestDTO.FechaPermiso,
                };

                Permissions permissionResult = await _unitOfWork.GeneralRepository<Permissions>().UpdateAsync(request.Id, permissions);

                _unitOfWork.Commit();

                return new PermissionsResultDTO
                {
                    NombreEmpleado = permissionResult.NombreEmpleado,
                    ApellidoEmpleado = permissions.ApellidoEmpleado,
                    TipoPermiso = permissionResult.TipoPermiso,
                    FechaPermiso = permissions.FechaPermiso,
                };
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                return null;
            }
        }
    }
}
