using System.Net.Http.Headers;
using ShareRecipe.Services.Common.Domain;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public class UserProfile : Entity, IAggregateRoot
    {
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }
    }
}