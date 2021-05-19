using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.Follower.Domain.Events
{
    public class FollowerCreatedDomainEvent : DomainEvent
    {
        public Guid FollowerId { get; }

        public FollowerCreatedDomainEvent(Guid followerId)
        {
            FollowerId = followerId;
            Version = 1;
        }
    }
}