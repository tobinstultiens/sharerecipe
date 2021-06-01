using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.FollowerService.Domain.Events
{
    public class FollowerCreatedDomainEvent : DomainEvent
    {
        public Guid FollowerId { get; }
        public Guid FollowingId { get; }

        public FollowerCreatedDomainEvent(Guid followerId, Guid followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
            Version = 1;
        }
    }
}