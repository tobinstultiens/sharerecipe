using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.IntegrationEvents.UserCreated
{
    public class CreatedUserDomainHandler : IConsumeAsync<CreatedUserIntegrationEvent>
    {
        private readonly IKweetRepository _kweetRepository;

        public CreatedUserDomainHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;
        }

        public async Task ConsumeAsync(CreatedUserIntegrationEvent message, CancellationToken cancellationToken)
        {
            ProfileAggregate aggregate = new ProfileAggregate(message.UserId, message.DisplayName, message.Image);
            _kweetRepository.Add(aggregate);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to create user");
        }
    }
}