using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Queries;
using N5NowTestBrayanVente.Controllers;

namespace N5NowTestBrayanVente.Test.Controllers
{
    public class PermissionControllerTest
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<PermissionController>> _logger;
        public PermissionControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<PermissionController>>();
        }

        [Fact]
        public void Get_InputId_ResultObject()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = new();
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            GetPermissionsQuery getPermissionsQuery = new(0);
            _mediator.Setup(x => x.Send(It.IsAny<GetPermissionsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Get(0);
            //Assert
            Assert.NotNull(result.Result);
            Assert.IsType<PermissionsResultDTO>(result.Result.Value);
        }

        [Fact]
        public void Get_InputId_ResultNotFound()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = null;
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            GetPermissionsQuery getPermissionsQuery = new(0);
            _mediator.Setup(x => x.Send(It.IsAny<GetPermissionsQuery>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Get(0);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result.Result);
        }

        [Fact]
        public void Create_InputPermisionRequestDTO_ResultObject()
        {
            //Arrege

            PermissionsResultDTO permissionsResultDTO = new();
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            _mediator.Setup(x => x.Send(It.IsAny<RequestPermissionCommand>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);
            PermisionRequestDTO permisionRequestDTO = new();
            RequestPermissionCommand requestPermissionCommand = new(permisionRequestDTO);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Create(requestPermissionCommand);
            //Assert
            Assert.NotNull(result.Result);
            Assert.IsType<PermissionsResultDTO>(result.Result.Value);
        }

        [Fact]
        public void Create_InputPermisionRequestDTONull_ResultNotFound()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = null;
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            _mediator.Setup(x => x.Send(It.IsAny<RequestPermissionCommand>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);
            PermisionRequestDTO permisionRequestDTO = new();
            RequestPermissionCommand requestPermissionCommand = new(permisionRequestDTO);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Create(requestPermissionCommand);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result.Result);
        }

        [Fact]
        public void Edit_InputPermisionRequestDTOAndId_ResultBadRequest()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = new();
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            _mediator.Setup(x => x.Send(It.IsAny<ModifyPermissionCommand>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);

            PermisionRequestDTO permisionRequestDTO = new();
            ModifyPermissionCommand modifyPermissionCommand = new(100, permisionRequestDTO);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Edit(0, modifyPermissionCommand);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public void Edit_InputPermisionRequestDTONullAndId_ResultNotFound()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = null;
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            _mediator.Setup(x => x.Send(It.IsAny<ModifyPermissionCommand>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);

            PermisionRequestDTO permisionRequestDTO = new();
            ModifyPermissionCommand modifyPermissionCommand = new(0, permisionRequestDTO);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Edit(0, modifyPermissionCommand);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void Edit_InputPermisionRequestDTOAndId_ResultOk()
        {
            //Arrege
            PermissionsResultDTO permissionsResultDTO = new();
            Task<PermissionsResultDTO> permissionsTask = Task.FromResult(permissionsResultDTO);
            _mediator.Setup(x => x.Send(It.IsAny<ModifyPermissionCommand>(), It.IsAny<CancellationToken>()))
                .Returns(permissionsTask);

            PermisionRequestDTO permisionRequestDTO = new();
            ModifyPermissionCommand modifyPermissionCommand = new(0, permisionRequestDTO);
            PermissionController permissionController = new(_mediator.Object, _logger.Object);
            //act
            var result = permissionController.Edit(0, modifyPermissionCommand);
            //Assert
            Assert.NotNull(result);
            Assert.IsType<OkResult>(result.Result);
        }
    }
}
