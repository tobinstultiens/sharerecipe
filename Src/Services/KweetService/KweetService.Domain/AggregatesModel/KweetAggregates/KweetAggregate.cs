using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class KweetAggregate : Entity, IAggregateRoot
    {
        public string Message { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<string> Directions { get; private set; }

        protected KweetAggregate()
        {
        }

        public KweetAggregate(Guid kweetId, string message, List<Ingredient> ingredients, List<string> directions)
        {
            SetId(kweetId);
            AddDomainEvent(new KweetCreatedDomainEvent(kweetId));
            SetMessage(kweetId, message);
            SetDirections(kweetId, directions);
        }

        private void SetDirections(Guid kweetId, List<string> directions)
        {
            Directions = directions;
            AddDomainEvent(new KweetDirectionsUpdatedDomainEvent(kweetId, directions));
        }

        private void SetMessage(Guid kweetId, string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message is empty");
            Message = message;
            AddDomainEvent(new KweetMessageUpdatedDomainEvent(kweetId, message));
        }
    }
}