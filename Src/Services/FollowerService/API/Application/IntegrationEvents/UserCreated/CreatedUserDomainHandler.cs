using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.AutoSubscribe;
using EasyNetQ.Consumer;
using MediatR;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.API.Application.IntegrationEvents.UserCreated
{
    public class CreatedUserDomainHandler : IConsumeAsync<CreatedUserIntegrationEvent>
    {
        private readonly IFollowerRepository _followerRepository;

        public CreatedUserDomainHandler(IFollowerRepository followerRepository)
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