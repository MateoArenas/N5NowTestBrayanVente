using MediatR;
using Microsoft.AspNetCore.Mvc;
using N5NowTestBrayanVente.Application.Commands;
using N5NowTestBrayanVente.Application.DTOs;
using N5NowTestBrayanVente.Application.Queries;

namespace N5NowTestBrayanVente.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<PermissionController> _logger;
        public PermissionController(IMediator mediator, ILogger<PermissionController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionsResultDTO>> Get(int id)
        {
            _logger.LogInformation("Iniciando ejecución de Get Permissions");

            var permission = await _mediator.Send(new GetPermissionsQuery(id));
            if (permission == null)
            {
                _logger.LogWarning("Get Permissions - No se encuentran datos");
                return NotFound();
            }

            _logger.LogInformation("Get Permissions - ejecutado exitosamente");
            return permission;
        }

        [HttpPost]
        public async Task<ActionResult<PermissionsResultDTO>> Create(RequestPermissionCommand requestPermissionCommand)
        {
            _logger.LogInformation("Iniciando ejecución de Request Permissions");

            PermissionsResultDTO permissionsResultDTO = await _mediator.Send(requestPermissionCommand);
            if (permissionsResultDTO == null)
            {
                _logger.LogWarning("Request Permissions - No se encuentran datos");
                return NotFound();
            }

            _logger.LogInformation("Request Permissions ejecutado exitosamente");
            return permissionsResultDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ModifyPermissionCommand modifyPermissionCommand)
        {
            _logger.LogInformation("Iniciando ejecución de Modify Permissions");

            if (id != modifyPermissionCommand.Id)
            {
                _logger.LogError("Modify Permissions - datos incorrectos");
                return BadRequest();
            }

            PermissionsResultDTO permissionsResultDTO = await _mediator.Send(modifyPermissionCommand);
            if (permissionsResultDTO == null)
            {
                _logger.LogWarning("Modify Permissions - No se encuentran datos");
                return NotFound();
            }

            _logger.LogInformation("Modify Permissions ejecutado exitosamente");
            return Ok();
        }
    }
}
