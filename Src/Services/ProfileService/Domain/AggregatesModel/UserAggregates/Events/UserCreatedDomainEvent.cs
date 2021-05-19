using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public sealed class UserCreatedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }

        public UserCreatedDomainEvent(Guid userId)
        {
            UserId = userId;
            Version = 1;
        }
    }
}