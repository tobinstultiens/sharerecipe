using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using ShareRecipe.Services.Common.API.IntegrationEvents;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.IntegrationEvents.UserDisplayNameUpdated
{
    public class UpdatedUserDisplayNameIntegrationHandler : IConsumeAsync<UpdatedUserDisplayNameIntegrationEvent>
    {
        private readonly IKweetRepository _kweetRepository;

        public UpdatedUserDisplayNameIntegrationHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;
        }

        public async Task ConsumeAsync(UpdatedUserDisplayNameIntegrationEvent message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregate = await _kweetRepository.GetAsync(message.UserId, cancellationToken);
            aggregate.SetDisplayName(message.DisplayName);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to update user display name");
        }
    }
}