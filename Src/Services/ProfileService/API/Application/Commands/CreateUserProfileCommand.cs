using System;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;

namespace ShareRecipe.Services.ProfileService.API.Application.Commands
{
    public class CreateUserProfileCommand : IRequest<CommandResponse>
    {
        public Guid UserId { get; set; }
        public string UserDisplayName { get; init; }
        public string UserProfileDescription { get; init; }
        public string UserProfileImage { get; init; }
    }
}