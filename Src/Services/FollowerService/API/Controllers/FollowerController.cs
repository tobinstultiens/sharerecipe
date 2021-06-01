using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Services.FollowerService.API.Application.Commands;

namespace ShareRecipe.Services.FollowerService.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FollowerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FollowerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreatedFollowerCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? new CreatedAtRouteResult(new {command.FollowerId}, response) : BadRequest(response);
        }
    }
}