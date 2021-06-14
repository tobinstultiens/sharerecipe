using System;
using System.Security.Claims;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Services.ProfileService.API.Application.Commands;

namespace ShareRecipe.Services.ProfileService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateAsync(CreateUserProfileCommand command)
        {
            var userid=HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            command.UserId = Guid.Parse(userid);
            var response = await _mediator.Send(command);
            return response.Success ? new CreatedAtRouteResult(new {command.UserId}, response) : BadRequest(response);
        }
    }
}