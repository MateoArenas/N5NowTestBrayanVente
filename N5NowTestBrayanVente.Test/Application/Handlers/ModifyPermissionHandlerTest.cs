﻿using Moq;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Handlers;
using N5NowTestBrayanVente.Domain.Aggregates.PermissionsAggregate.Models;
using N5NowTestBrayanVente.Domain.Enums;
using N5NowTestBrayanVente.Infrastructure.Remotes;
using N5NowTestBrayanVente.Infrastructure.Repositories;

namespace N5NowTestBrayanVente.Test.Application.Handlers
{
    public class ModifyPermissionHandlerTest
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IGeneralRepository<Permissions>> _generalRepository;
        private readonly Mock<IKafkaProducer> _kafkaProducer;

        public ModifyPermissionHandlerTest()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _kafkaProducer = new Mock<IKafkaProducer>();
            _generalRepository = new Mock<IGeneralRepository<Permissions>>();
        }

        [Fact]
        public void Handle_InputModifyPermissionCommand_returnObject()
        {
            //Arrage
            PermisionRequestDTO request = new();
            ModifyPermissionCommand modifyPermissionCommand = new(0, request);
            Permissions permissions = new Permissions();
            Task<Permissions> permissionsTask = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            _generalRepository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Permissions>())).Returns(permissionsTask);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>()).Returns(_generalRepository.Object);
            _unitOfWork.Setup(x => x.Commit());
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            ModifyPermissionHandler modifyPermissionHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = modifyPermissionHandler.Handle(modifyPermissionCommand, CancellationToken.None);
            //Asserts
            Assert.NotNull(Result.Result);
        }

        [Fact]
        public void Handle_InputModifyPermissionCommand_returnNull()
        {
            //Arrage
            PermisionRequestDTO request = new();
            ModifyPermissionCommand modifyPermissionCommand = new(0, request);
            Permissions permissions = null;
            Task<Permissions> permissionsTask = Task.FromResult(permissions);
            Task<bool> kafkaResult = Task.FromResult(true);
            _generalRepository.Setup(x => x.UpdateAsync(It.IsAny<int>(), It.IsAny<Permissions>())).Returns(permissionsTask);
            _unitOfWork.Setup(x => x.GeneralRepository<Permissions>()).Returns(_generalRepository.Object);
            _unitOfWork.Setup(x => x.Commit());
            _kafkaProducer.Setup(x => x.ProduceKafkaMessage(It.IsAny<KafkaMessageEnum>())).Returns(kafkaResult);
            ModifyPermissionHandler modifyPermissionHandler = new(_unitOfWork.Object, _kafkaProducer.Object);
            //Act
            var Result = modifyPermissionHandler.Handle(modifyPermissionCommand, CancellationToken.None);
            //Asserts
            Assert.Null(Result.Result);
        }
    }
}
