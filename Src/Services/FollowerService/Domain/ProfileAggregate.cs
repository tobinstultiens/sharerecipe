using System;
using System.Collections.Generic;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.Follower.Domain.Events;

namespace ShareRecipe.Services.Follower.Domain
{
    public class ProfileAggregate : Entity, IAggregateRoot
    {
        public List<Follower> Followers { get; private set; }
        public List<Follower> Following { get; private set; }
        public string DisplayName { get; private set; }
        public string ProfilePictureUrl { get; private set; }
        
        protected ProfileAggregate()
        {
            Followers = new List<Follower>();
            Following = new List<Follower>();
        }

        public ProfileAggregate(Guid userid, string displayName, string profilePictureUrl)
        {
            SetId(userid);
            SetDisplayName(displayName);
            SetProfilePictureUrl(profilePictureUrl);
        }

        public void SetProfilePictureUrl(string image)
        {
            if (string.IsNullOrWhiteSpace(image))
                throw new ArgumentException("The image is null, empty or contains a whitespace.");
            ProfilePictureUrl = image;
        }

        public void SetDisplayName(string displayName)
        {
            if (string.IsNullOrWhiteSpace(displayName))
                throw new ArgumentException("Display name is empty.");
            if (displayName.Length > 30)
                throw new ArgumentException("The length of the display name has exceed the allowed characters");
            DisplayName = displayName ?? throw new ArgumentNullException(nameof(displayName));
        }

        public bool Follow(ProfileAggregate otherAggregate)
        {
            if (otherAggregate == default)
                throw new ArgumentException("The user cannot be null");
            if (otherAggregate.Id == Guid.Empty)
                throw new ArgumentException("userid cannot be empty");
            Follower follow = Following.Find(follow => follow.FollowerId == Id && follow.FollowingId == otherAggregate.Id);
            if (follow != default)
                return false;
            follow = new(Id, otherAggregate.Id);
            otherAggregate.AddFollower(follow);
            Following.Add(follow);
            return true;
        }
        
        public bool UnFollow(ProfileAggregate otherAggregate)
        {
            if (otherAggregate == default)
                throw new ArgumentException("The user cannot be null");
            if (otherAggregate.Id == Guid.Empty)
                throw new ArgumentException("userid cannot be empty");
            Follower follow = Following.Find(foll => foll.FollowerId == Id && foll.FollowingId == otherAggregate.Id);
            if (follow == default)
                return false;
            otherAggregate.RemoveFollower(follow);
            Following.Remove(follow);
            return true;
        }

        private void RemoveFollower(Follower follow)
        {
            Followers.Remove(follow);
        }

        private void AddFollower(Follower follow)
        {
            Followers.Add(follow);
        }
    }
}