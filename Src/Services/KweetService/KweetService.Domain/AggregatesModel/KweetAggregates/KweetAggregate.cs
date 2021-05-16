using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class KweetAggregate : Entity, IAggregateRoot
    {
        public Guid userId { get; private set; }
        public string Message { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<string> Directions { get; private set; }

        protected KweetAggregate()
        {
        }

        public KweetAggregate(Guid userId, string message, List<Ingredient> ingredients, List<string> directions)
        {
            SetId(Guid.NewGuid());
            SetUserId(userId);
            AddDomainEvent(new KweetCreatedDomainEvent(userId));
            SetMessage(userId, message);
            SetDirections(userId, directions);
        }

        private void SetUserId(Guid guid)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("The user id is empty.");
            Id = guid;
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