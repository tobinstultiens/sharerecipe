using System;
using System.Net.Http.Headers;
using ShareRecipe.Services.Common.Domain;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public class UserProfile : Entity, IAggregateRoot
    {
        public string DisplayName { get; private set; }
        public string Description { get; private set; }
        public string Image { get; private set; }

        public UserProfile(string displayName, string description, string image)
        {
            SetDisplayName(displayName);
            Description = description;
            Image = image;
        }
        
        public void SetDisplayName(string displayName)
        {
            DisplayName = displayName?? throw new ArgumentNullException(nameof(displayName));
        }
    }
}