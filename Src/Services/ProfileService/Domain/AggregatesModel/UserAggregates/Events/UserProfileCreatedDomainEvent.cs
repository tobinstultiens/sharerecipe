using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public sealed class UserProfileCreatedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserProfileCreatedDomainEvent(Guid userId)
        {
            UserId = userId;
            Version = 1;
        }
    }
}