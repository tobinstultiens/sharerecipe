using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.API.Application.Commands
{
    public sealed class CreatedFollowerCommandHandler : IRequestHandler<CreatedFollowerCommand, CommandResponse>
    {
        private readonly IFollowerRepository _followerRepository;

        public CreatedFollowerCommandHandler(IFollowerRepository followerRepository)
        {
            _followerRepository =
                followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
        }

        public async Task<CommandResponse> Handle(CreatedFollowerCommand request, CancellationToken cancellationToken)
        {
            var aggregate = await _followerRepository.Find(request.FollowerId);
            var otherAggregate = await _followerRepository.Find(request.FollowedId);

            var followed = aggregate.Follow(otherAggregate);
            var success = await _followerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            return new CommandResponse
            {
                Success = success && followed
            };
        }
    }
}