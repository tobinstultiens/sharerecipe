using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates.Events
{
    public class UserProfileImageUpdateDomainEvent : DomainEvent
    {
        public Guid UserId { get; }
        public string Image { get; }

        public UserProfileImageUpdateDomainEvent(Guid userId, string image)
        {
            UserId = userId;
            Image = image;
        }
    }
}