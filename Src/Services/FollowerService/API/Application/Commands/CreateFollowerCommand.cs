using System;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;

namespace ShareRecipe.Services.Follower.API.Application.Commands
{
    public class CreateFollowerCommand : IRequest<CommandResponse>
    {
        public Guid FollowerId { get; init; }
        public Guid FollowedId { get; init; }
    }
}