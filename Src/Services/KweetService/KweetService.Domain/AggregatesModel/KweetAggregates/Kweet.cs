using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class Kweet : Entity
    {
        public Guid UserId { get; private set; }
        public string Message { get; private set; }
        public List<Ingredient> Ingredients { get; private set; }
        public List<Direction> Directions { get; private set; }

        protected Kweet()
        {
        }

        public Kweet(string message, List<Ingredient> ingredients, List<Direction> directions)
        {
            SetId(Guid.NewGuid());
            SetMessage(Id, message);
            SetDirections(Id, directions);
            SetIngredients(Id, ingredients);
            AddDomainEvent(new KweetCreatedDomainEvent(Id));
        }

        private void SetIngredients(Guid guid, List<Ingredient> ingredients)
        {
            Ingredients = ingredients;
            AddDomainEvent(new KweetIngredientsUpdatedDomainEvent(guid, ingredients));
        }

        private void SetDirections(Guid kweetId, List<Direction> directions)
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