using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetIngredientsUpdatedDomainEvent: DomainEvent
    {
        public Guid KweetId { get; }
        public List<Ingredient> Ingredients { get; }

        public KweetIngredientsUpdatedDomainEvent(Guid kweetId, List<Ingredient> ingredients)
        {
            KweetId = kweetId;
            Ingredients = ingredients;
            Version = 1;
        }
    }
}