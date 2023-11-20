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
        public PermissionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IList<PermissionsResultDTO>> GetAll()
        {
            return await _mediator.Send(new GetAllPermissionsQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PermissionsResultDTO>> Get(int id)
        {
            var permission = await _mediator.Send(new GetPermissionsQuery(id));
            if (permission == null)
            {
                return NotFound();
            }

            return await _mediator.Send(new GetPermissionsQuery(id));
        }

        [HttpPost]
        public async Task<ActionResult<PermissionsResultDTO>> Create(RequestPermissionCommand requestPermissionCommand)
        {
            PermissionsResultDTO permissionsResultDTO = await _mediator.Send(requestPermissionCommand);
            
            return permissionsResultDTO;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, ModifyPermissionCommand modifyPermissionCommand)
        {
            if (id != modifyPermissionCommand.Id)
            {
                return BadRequest();
            }

            PermissionsResultDTO permissionsResultDTO = await _mediator.Send(modifyPermissionCommand);
            if (permissionsResultDTO == null)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
