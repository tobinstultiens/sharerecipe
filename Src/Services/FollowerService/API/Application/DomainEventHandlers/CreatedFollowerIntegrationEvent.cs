using System;
using ShareRecipe.Services.Common.Domain.Events;

namespace ShareRecipe.Services.Follower.API.Application.DomainEventHandlers
{
    public class CreatedFollowerIntegrationEvent : DomainEvent
    {
        public Guid FollowerId { get; }
        public Guid FollowingId { get; }

        public CreatedFollowerIntegrationEvent(Guid followerId, Guid followingId)
        {
            FollowerId = followerId;
            FollowingId = followingId;
            Version = 1;
        }
    }
}