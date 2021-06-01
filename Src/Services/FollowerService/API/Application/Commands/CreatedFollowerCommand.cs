using System;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;

namespace ShareRecipe.Services.FollowerService.API.Application.Commands
{
    public class CreatedFollowerCommand : IRequest<CommandResponse>
    {
        public Guid FollowerId { get; init; }
        public Guid FollowedId { get; init; }
    }
}