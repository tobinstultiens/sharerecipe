using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetCreatedDomainEvent : DomainEvent
    {
        public Guid KweetId { get; }

        public KweetCreatedDomainEvent(Guid kweetId)
        {
            KweetId = kweetId;
            Version = 1;
        }
    }
}