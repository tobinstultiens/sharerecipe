using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public sealed class UserDisplayNameUpdatedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string DisplayName { get; }

        public UserDisplayNameUpdatedDomainEvent(Guid userId, string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
            Version = 1;
        }
    }
}