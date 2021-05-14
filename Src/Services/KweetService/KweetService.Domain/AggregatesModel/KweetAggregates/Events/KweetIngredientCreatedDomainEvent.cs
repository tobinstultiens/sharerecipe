using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetIngredientCreatedDomainEvent: DomainEvent
    {
        public Guid KweetId { get; }

        public KweetIngredientCreatedDomainEvent(Guid kweetId)
        {
            KweetId = kweetId;
            Version = 1;
        }
    }
}