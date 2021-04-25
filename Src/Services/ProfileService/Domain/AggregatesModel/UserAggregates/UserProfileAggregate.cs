using System;
using ShareRecipe.Services.Common.Domain;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public abstract class UserProfileAggregate: Entity, IAggregateRoot
    {
        public string DisplayName { get; private set; }
        public UserProfile UserProfile { get; private set; }

        public UserProfileAggregate(Guid userId, string displayName, string description, string image)
        {
            SetId(userId);
            SetDisplayName(displayName);
            UserProfile = new UserProfile(description, image);
        }
        
        public void SetDisplayName(string displayName)
        {
            DisplayName = displayName?? throw new ArgumentNullException(nameof(displayName));
        }
        
        private void SetId(Guid userId)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException("The user id is empty.");
            Id = userId;
        }
    }
}