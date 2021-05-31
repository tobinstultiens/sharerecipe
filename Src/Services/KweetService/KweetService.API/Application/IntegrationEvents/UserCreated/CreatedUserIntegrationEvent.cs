using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.API.Application.IntegrationEvents.UserCreated
{
    public class CreatedUserIntegrationEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string DisplayName { get; }
        public string Description { get; }
        public string Image { get; }

        public CreatedUserIntegrationEvent(Guid userId, string displayName, string description, string image)
        {
            UserId = userId;
            DisplayName = displayName;
            Description = description;
            Image = image;
            Version = 1;
        }
    }
}