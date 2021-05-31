using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.API.Application.IntegrationEvents.UserImageUpdated
{
    public class UpdatedUserImageIntegrationEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Image { get; }

        public UpdatedUserImageIntegrationEvent(Guid userId, string image)
        {
            UserId = userId;
            Image = image;
            Version = 1;
        }
    }
}