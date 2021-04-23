using System;
using MediatR;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public record UserCreatedDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string DisplayName { get; }

        public UserCreatedDomainEvent(Guid userId, string displayName)
        {
            UserId = userId;
            DisplayName = displayName;
            Version = 1;
        }
    }
}