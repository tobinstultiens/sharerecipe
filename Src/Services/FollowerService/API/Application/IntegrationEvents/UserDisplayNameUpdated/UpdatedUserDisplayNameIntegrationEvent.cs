using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.FollowerService.API.Application.IntegrationEvents.UserDisplayNameUpdated
{
    public class UpdatedUserDisplayNameIntegrationEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string DisplayName { get; }

        public UpdatedUserDisplayNameIntegrationEvent(Guid userId, string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
            Version = 1;
        }
    }
}