using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetDirectionsUpdatedDomainEvent: DomainEvent
    {
        public Guid KweetId { get; }
        public List<Direction> Directions { get; }

        public KweetDirectionsUpdatedDomainEvent(Guid kweetId, List<Direction> directions)
        {
            KweetId = kweetId;
            Directions = directions;
            Version = 1;
        }
    }
}