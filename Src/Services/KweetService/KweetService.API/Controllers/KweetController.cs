using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Services.KweetService.API.Application.Commands.CreateKweet;
using ShareRecipe.Services.KweetService.API.Application.Queries.GetKweet;

namespace ShareRecipe.Services.KweetService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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
            var userid=HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = Guid.Parse(userid);
            var response = await _mediator.Send(command);
            return response.Success ? new CreatedAtRouteResult(new {command.UserId}, response) : BadRequest(response);
        }
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync([FromQuery] int page, [FromQuery] int size)
        {
            var command = new GetKweetCommand{ page = page, size = size};
            var response = await _mediator.Send(command);
            return response != null ? new OkObjectResult(response) : new BadRequestObjectResult(response);
        }
    }
}
