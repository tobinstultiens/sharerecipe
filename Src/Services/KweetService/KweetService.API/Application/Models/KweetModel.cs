using System;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.Models
{
    public class KweetModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string DisplayName { get; set; }
        public string Image { get; set; }
        public Kweet Kweet { get; set; }
    }
}