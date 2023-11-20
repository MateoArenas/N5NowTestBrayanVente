using MediatR;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Queries;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Enums;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;

namespace N5NowTestBrayanVente.Application.Handlers
{
    public class GetPermissionsHandler : IRequestHandler<GetPermissionsQuery, PermissionsResultDTO>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IKafkaProducer _kafkaProducer;
        public GetPermissionsHandler(IUnitOfWork unitOfWork, IKafkaProducer kafkaProducer)
        {
            _unitOfWork = unitOfWork;
            _kafkaProducer = kafkaProducer;
        }
        public async Task<PermissionsResultDTO> Handle(GetPermissionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                await _kafkaProducer.ProduceKafkaMessage(KafkaMessageEnum.Get);

                Permissions? permission = await _unitOfWork.GeneralRepository<Permissions>().GetAsync(request.Id);

                if (permission == null)
                    return null;

                return new PermissionsResultDTO
                {
                    NombreEmpleado = permission.NombreEmpleado,
                    ApellidoEmpleado = permission.ApellidoEmpleado,
                    TipoPermiso = permission.TipoPermiso,
                    FechaPermiso = permission.FechaPermiso,
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
