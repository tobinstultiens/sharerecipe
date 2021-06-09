using System;
using EasyNetQ;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.Common.API.IntegrationEvents
{
    [Queue("UpdatedUserDisplayNameIntegrationEvent", ExchangeName = "UpdatedUserDisplayNameIntegrationExchange")]
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