using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Services.KweetService.API.Application.Commands;

namespace ShareRecipe.Services.KweetService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KweetController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KweetController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateKweetCommand command)
        {
            var response = await _mediator.Send(command);
            return response.Success ? new CreatedAtRouteResult(new {command.UserId}, response) : BadRequest(response);
        }
    }
}
