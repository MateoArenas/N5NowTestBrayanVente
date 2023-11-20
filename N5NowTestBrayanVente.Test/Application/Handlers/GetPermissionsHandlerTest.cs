using Moq;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Handlers;
using N5NowTestBrayanVente.Application.Queries;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Enums;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N5NowTestBrayanVente.Test.Application.Handlers
{
    public class GetPermissionsHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGeneralRepository<Permissions>> _generalRepository;
        private readonly Mock<IKafkaProducer> _kafkaProducer;

        public GetPermissionsHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _kafkaProducer = new Mock<IKafkaProducer>();
            _generalRepository = new Mock<IGeneralRepository<Permissions>>();          
        }

        [Fact]
        public void Handle_InputGetPermissionsQuery_returnObject()
        {
            //A
            GetPermissionsQuery getPermissionsQuery = new(0);
            Permissions permissions = new Permissions();
            Task<Permissions> permissionsResultDTO = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            _generalRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(permissionsResultDTO);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>()).Returns(_generalRepository.Object);
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            GetPermissionsHandler getPermissionsHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = getPermissionsHandler.Handle(getPermissionsQuery, CancellationToken.None);
            //Asserts
            Assert.NotNull(Result.Result);
        }

        [Fact]
        public void Handle_InputGetPermissionsQuery_returnNull()
        {
            //A
            GetPermissionsQuery getPermissionsQuery= new(0);
            Permissions permissions = null;
            Task<Permissions> permissionsResultDTO = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            //_generalRepository.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(permissionsResultDTO);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>().GetAsync(It.IsAny<int>())).Returns(permissionsResultDTO);
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            GetPermissionsHandler getPermissionsHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = getPermissionsHandler.Handle(getPermissionsQuery, CancellationToken.None);
            //Asserts
            Assert.Null(Result.Result);
        }
    }
}
