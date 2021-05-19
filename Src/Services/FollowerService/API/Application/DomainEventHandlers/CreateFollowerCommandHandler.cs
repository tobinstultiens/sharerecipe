using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.Follower.API.Application.Commands;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.API.Application.DomainEventHandlers
{
    public sealed class CreateFollowerCommandHandler : IRequestHandler<CreateFollowerCommand, CommandResponse>
    {
        private readonly IFollowerRepository _followerRepository;

        public CreateFollowerCommandHandler(IFollowerRepository followerRepository)
        {
            _followerRepository =
                followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
        }

        public async Task<CommandResponse> Handle(CreateFollowerCommand request, CancellationToken cancellationToken)
        {
            FollowerAggregate followerAggregate =
                new(request.FollowerId, request.FollowedId, DateTime.UtcNow);

            _followerRepository.Add(followerAggregate);

            bool success = await _followerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            
            CommandResponse commandResponse = new()
            {
                Success = success
            };

            return commandResponse; 
        }
    }
}