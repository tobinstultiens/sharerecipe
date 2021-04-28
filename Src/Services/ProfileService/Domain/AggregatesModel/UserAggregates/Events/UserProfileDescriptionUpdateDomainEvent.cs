using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public sealed class UserProfileDescriptionUpdateDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Description { get; }

        public UserProfileDescriptionUpdateDomainEvent(Guid id, string description)
        {
            UserId = id;
            Description = description;
        }
    }
}