using System;
using EasyNetQ;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.Common.API.IntegrationEvents
{
    [Queue("UpdatedUserImageIntegrationEvent", ExchangeName = "UpdatedUserImageIntegrationExchange")]
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