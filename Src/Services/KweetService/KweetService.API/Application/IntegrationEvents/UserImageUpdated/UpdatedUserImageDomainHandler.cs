using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.IntegrationEvents.UserImageUpdated
{
    public class UpdatedUserImageDomainHandler : IConsumeAsync<UpdatedUserImageIntegrationEvent>
    {
        private readonly IKweetRepository _kweetRepository;

        public UpdatedUserImageDomainHandler(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository;
        }

        public async Task ConsumeAsync(UpdatedUserImageIntegrationEvent message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregate = await _kweetRepository.GetAsync(message.UserId, cancellationToken);
            aggregate.SetProfilePictureUrl(message.Image);
            bool success = await _kweetRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to update user image");
        }
    }
}