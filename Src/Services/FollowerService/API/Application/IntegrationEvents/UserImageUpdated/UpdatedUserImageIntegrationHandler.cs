using System;
using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;
using ShareRecipe.Services.FollowerService.Domain;

namespace ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserImageUpdated
{
    public class UpdatedUserImageIntegrationHandler : IConsumeAsync<UpdatedUserImageIntegrationEvent>
    {
        private readonly IFollowerRepository _followerRepository;

        public UpdatedUserImageIntegrationHandler(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository;
        }

        public async Task ConsumeAsync(UpdatedUserImageIntegrationEvent message,
            CancellationToken cancellationToken = new CancellationToken())
        {
            var aggregate = await _followerRepository.Find(message.UserId);
            aggregate.SetProfilePictureUrl(message.Image);
            bool success = await _followerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
            if (!success)
                throw new Exception("Failed to update user image");
        }
    }
}