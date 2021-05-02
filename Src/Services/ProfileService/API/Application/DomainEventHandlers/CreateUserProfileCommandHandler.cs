using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.ProfileService.Application.Application.Commands;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Application.Application.DomainEventHandlers
{
    public sealed class CreateUserProfileCommandHandler : IRequestHandler<CreateUserProfileCommand, CommandResponse>
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public CreateUserProfileCommandHandler(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository =
                userProfileRepository ?? throw new ArgumentNullException(nameof(userProfileRepository));
        }

        public async Task<CommandResponse> Handle(CreateUserProfileCommand request, CancellationToken cancellationToken)
        {
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