using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using ShareRecipe.Services.FollowerService.Domain;

namespace ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserCreated
{
    public class CreatedUserIntegrationHandler : IConsumeAsync<CreatedUserIntegrationEvent>
    {
        private readonly IFollowerRepository _followerRepository;

        public CreatedUserIntegrationHandler(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task ConsumeAsync(CreatedUserIntegrationEvent message, CancellationToken cancellationToken)
        {
            ProfileAggregate aggregate = new ProfileAggregate(message.UserId, message.DisplayName, message.Image);
            _followerRepository.Add(aggregate);
            bool success = await _followerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to create user");
        }
    }
}