using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetMessageUpdatedDomainEvent: DomainEvent
    {
        public Guid KweetId { get; }
        public string Message { get; }

        public KweetMessageUpdatedDomainEvent(Guid kweetId, string message)
        {
            KweetId = kweetId;
            Message = message;
            Version = 1;
        }
    }
}