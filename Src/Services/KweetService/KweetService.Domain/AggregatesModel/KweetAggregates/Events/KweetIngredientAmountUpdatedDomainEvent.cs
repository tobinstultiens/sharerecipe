using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events
{
    public class KweetIngredientAmountUpdatedDomainEvent : DomainEvent
    {
        public Guid KweetId { get; }
        public string Amount { get; }

        public KweetIngredientAmountUpdatedDomainEvent(Guid kweetId, string amount)
        {
            KweetId = kweetId;
            Amount = amount;
            Version = 1;
        }
    }
}