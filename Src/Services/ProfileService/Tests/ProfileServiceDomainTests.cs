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
        public void UserProfileTestEmptyImage()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "askdfj", "description", "");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestEmptyDisplay()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "", "description", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestExceedingLengthDisplay()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "ASDFKLASLDKFASKLJDFFSADLJKFSADFKJASDJKLFSKALDJFFJKLASDFLKJSADFlkJASDFLKJASDFJKLASDFLJKASDLKJASFDJLK", "description", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestExceedingLengthDescription()
        {
            Action act = () => new UserProfileAggregate(Guid.NewGuid(), "asdfasdf", "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nullam est arcu, congue in mattis vitae, pharetra eu felis. Phasellus porttitor tortor id tincidunt congue. Maecenas lacinia, orci sed ullamcorper euismod, elit urna consequat nulla, eu ultricies nunc nunc sed libero. Nullam volutpat fringilla tristique. Suspendisse potenti. Nullam blandit placerat elit a volutpat. Quisque finibus rutrum neque, non sollicitudin dui ullamcorper ac. Fusce laoreet mattis sapien. Sed iaculis elit et nibh auctor iaculis. Morbi sed scelerisque nisl, quis euismod ligula. In ultricies placerat nulla id mattis. Maecenas quis erat vitae ipsum vehicula aliquet eu eu metus. Proin varius viverra ipsum, non.", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
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