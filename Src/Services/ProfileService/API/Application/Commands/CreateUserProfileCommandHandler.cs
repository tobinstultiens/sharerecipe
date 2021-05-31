using System;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using MediatR;
using Microsoft.AspNetCore.Http;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.API.Application.Commands
{
    public sealed class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, CommandResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateUserProfileCommandHandler(IUserProfileRepository userProfileRepository, IHttpContextAccessor httpContextAccessor)
        {
            _userProfileRepository =
                userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CommandResponse> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
            //TODO check if this is comparable
            var userid=_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            UserProfileAggregate userProfileAggregate =
                new(request.UserId, request.UserDisplayName, request.UserProfileDescription, request.UserProfileImage);

            _userProfileRepository.Add(userProfileAggregate);

            bool success = await _userProfileRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            CommandResponse commandResponse = new()
            {
                Success = success
            };

            return commandResponse; 
        }
    }
}