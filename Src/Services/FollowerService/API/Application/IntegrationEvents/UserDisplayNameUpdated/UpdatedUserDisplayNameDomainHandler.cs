using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using MediatR;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.API.Application.IntegrationEvents.UserDisplayNameUpdated
{
    public class UpdatedUserDisplayNameDomainHandler : IConsumeAsync<UpdatedUserDisplayNameIntegrationEvent>
    {
        private readonly IFollowerRepository _followerRepository;

        public UpdatedUserDisplayNameDomainHandler(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task ConsumeAsync(UpdatedUserDisplayNameIntegrationEvent message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregate = await _followerRepository.Find(message.UserId);
            aggregate.SetDisplayName(message.DisplayName);
            bool success = await _followerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to update user display name");
        }
    }
}