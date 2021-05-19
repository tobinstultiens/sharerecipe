using System;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.Follower.Domain.Events;

namespace ShareRecipe.Services.Follower.Domain
{
    public class FollowerAggregate : Entity, IAggregateRoot
    {
        public Guid FollowerId { get; private set; }
        public Guid FollowedId { get; private set; }
        public DateTime FollowedAt { get; private set; }

        protected FollowerAggregate()
        {
        }

        public FollowerAggregate(Guid followerId, Guid followedId, DateTime followedAt)
        {
            SetFollowerId(followerId);
            SetFollowedId(followedId);
            SetFollowedAt(followedAt);
            AddDomainEvent(new FollowerCreatedDomainEvent(followerId));
        }

        private void SetFollowedAt(DateTime followedAt)
        {
            FollowedAt = followedAt;
        }

        private void SetFollowedId(Guid followedId)
        {
            if (followedId == Guid.Empty)
                throw new ArgumentException("The followed id is empty.");
            FollowedId = followedId;
        }

        private void SetFollowerId(Guid followerId)
        {
            if (followerId == Guid.Empty)
                throw new ArgumentException("The follower id is empty.");
            FollowerId = followerId;
        }
    }
}