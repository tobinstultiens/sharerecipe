using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetIngredientDescriptionUpdatedDomainEvent :DomainEvent
    {
        public Guid KweetId { get; }
        public string Description { get; }

        public KweetIngredientDescriptionUpdatedDomainEvent(Guid kweetId, string description)
        {
            KweetId = kweetId;
            Description = description;
            Version = 1;
        }
    }
}