using Moq;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Handlers;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Enums;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;

namespace N5NowTestBrayanVente.Test.Application.Handlers
{
    public class RequestPermissionHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGeneralRepository<Permissions>> _generalRepository;
        private readonly Mock<IKafkaProducer> _kafkaProducer;

        public RequestPermissionHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _kafkaProducer = new Mock<IKafkaProducer>();
            _generalRepository = new Mock<IGeneralRepository<Permissions>>();
        }

        [Fact]
        public void Handle_InputRequestPermissionCommand_returnObject()
        {
            //Arrage
            PermisionRequestDTO request = new();
            RequestPermissionCommand requestPermissionCommand = new(request);
            Permissions permissions = new Permissions();
            Task<Permissions> permissionsTask = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            _generalRepository.Setup(x => x.InsertAsync(It.IsAny<Permissions>())).Returns(permissionsTask);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>()).Returns(_generalRepository.Object);
            _unitOfWork.Setup(x => x.Commit());
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            RequestPermissionHandler requestPermissionHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = requestPermissionHandler.Handle(requestPermissionCommand, CancellationToken.None);
            //Asserts
            Assert.NotNull(Result.Result);
        }

        [Fact]
        public void Handle_InputRequestPermissionCommand_returnNull()
        {
            //Arrage
            PermisionRequestDTO request = new();
            RequestPermissionCommand requestPermissionCommand = new(request);
            Permissions permissions = null;
            Task<Permissions> permissionsTask = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            _generalRepository.Setup(x => x.InsertAsync(It.IsAny<Permissions>())).Returns(permissionsTask);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>()).Returns(_generalRepository.Object);
            _unitOfWork.Setup(x => x.Commit());
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            RequestPermissionHandler requestPermissionHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = requestPermissionHandler.Handle(requestPermissionCommand, CancellationToken.None);
            //Asserts
            Assert.Null(Result.Result);
        }
    }
}
