using System;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates.Events;
using UnitsNet;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class Ingredient : Entity
    {
        public string Description { get; private set; }
        public string Amount { get; private set; }

        protected Ingredient()
        {
        }

        public Ingredient(string description, string amount)
        {
            SetId(Id);
            SetDescription(Id, description);
            SetAmount(Id, amount);
        }

        private void SetDescription(Guid id, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentException("Ingredient description was null or empty");
            Description = description;
            AddDomainEvent(new KweetIngredientDescriptionUpdatedDomainEvent(id, description));
        }

        private void SetAmount(Guid id, string amount)
        {
            Amount = amount;
            AddDomainEvent(new KweetIngredientAmountUpdatedDomainEvent(id, amount));
        }
    }
}