using System;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.FollowerService.Domain.Events;

namespace ShareRecipe.Services.FollowerService.Domain
{
    public class Follower : Entity
    {
        public Guid FollowerId { get; private set; }
        public Guid FollowingId { get; private set; }
        public DateTime FollowedAt { get; private set; }

        protected Follower()
        {
            
        }

        public Follower(Guid followerId, Guid following)
        {
            SetFollowerId(followerId);
            SetFollowingId(following, followerId);
            SetFollowedAt(DateTime.UtcNow);
            AddDomainEvent(new FollowerCreatedDomainEvent(followerId, following));
        }

        private void SetFollowedAt(DateTime followedAt)
        {
            FollowedAt = followedAt;
        }

        private void SetFollowingId(Guid followedId, Guid followerId)
        {
            if (followedId == Guid.Empty)
                throw new ArgumentException("The followed id is empty.");
            if (followedId == followerId)
                throw new ArgumentException("The followed and follower can't be the same person");
            FollowingId = followedId;
        }

        private void SetFollowerId(Guid followerId)
        {
            if (followerId == Guid.Empty)
                throw new ArgumentException("The follower id is empty.");
            FollowerId = followerId;
        }
    }
}