using System;
using FluentAssertions;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;
using Xunit;

namespace ShareRecipe.Services.ProfileService.Tests
{
    public class ProfileServiceDomainTests
    {
        [Fact]
        public void UserProfileTestCorrect()
        {
            var userProfileAggregate = new UserProfileAggregate(Guid.NewGuid(), "test", "description", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            userProfileAggregate.DisplayName.Should().Be("test");
            userProfileAggregate.UserProfile.Description.Should().Be("description");
            userProfileAggregate.UserProfile.Image.Should().Be("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            userProfileAggregate.DomainEvents.Should().NotBeEmpty().And.HaveCount(2);
            userProfileAggregate.UserProfile.DomainEvents.Should().NotBeEmpty().And.HaveCount(3);
        }

        [Fact]
        public void UserProfileTestInvalidUrl()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "test", "description", "image");
            act.Should().Throw<UriFormatException>();
        }
        
        [Fact]
        public void UserProfileTestInvalidImage()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "test", "description", "https://fluentassertions.com/collections/");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestEmptyDisplay()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "", "description", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestEmptyDescription()
        {
            var userProfileAggregate = new UserProfileAggregate(Guid.NewGuid(), "test", "", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            userProfileAggregate.DisplayName.Should().Be("test");
            userProfileAggregate.UserProfile.Description.Should().Be("");
            userProfileAggregate.UserProfile.Image.Should().Be("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            userProfileAggregate.DomainEvents.Should().NotBeEmpty().And.HaveCount(2);
            userProfileAggregate.UserProfile.DomainEvents.Should().NotBeEmpty().And.HaveCount(3);
        }
    }
}