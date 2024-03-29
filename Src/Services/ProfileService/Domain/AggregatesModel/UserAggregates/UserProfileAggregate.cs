using System;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public class UserProfileAggregate : Entity, IAggregateRoot
    {
        public string DisplayName { get; private set; }
        public UserProfile UserProfile { get; private set; }

        protected UserProfileAggregate() { }

        public UserProfileAggregate(Guid userId, string displayName, string description, string image)
        {
            SetId(userId);
            SetDisplayName(displayName);
            UserProfile = new UserProfile(userId, description, image);
            AddDomainEvent(new UserCreatedDomainEvent(Id, displayName, description, image));
        }

        public void SetDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name is empty.");
            if (displayName.Length > 30)
                throw new ArgumentException("The length of the display name has exceed the allowed characters");
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
            AddDomainEvent(new UserDisplayNameUpdatedDomainEvent(Id, displayName));
        }
    }
}