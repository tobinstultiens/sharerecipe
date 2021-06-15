using System;
using ShareRecipe.Services.Common.Domain;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public class Direction : Entity
    {
        public int Order { get; private set; }
        public string Message { get; private set; }

        protected Direction()
        {
            
        }

        public Direction(int order, string message)
        {
            SetId(Guid.NewGuid());
            Order = order;
            Message = message;
        }
    }
}