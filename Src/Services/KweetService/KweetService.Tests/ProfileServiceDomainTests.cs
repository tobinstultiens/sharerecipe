using System;
using FluentAssertions;
using ShareRecipe.Services.FollowerService.Domain;
using Xunit;

namespace ShareRecipe.Services.KweetService.Tests
{
    public class ProfileServiceDomainTests
    {
        [Fact]
        public void UserProfileTestCorrect()
        {
            var userProfileAggregate = new ProfileAggregate(Guid.NewGuid(), "test", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            userProfileAggregate.DisplayName.Should().Be("test");
            userProfileAggregate.ProfilePictureUrl.Should().Be("https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
        }

        [Fact]
        public void UserProfileTestEmptyImage()
        {
            Action act = () => new ProfileAggregate(Guid.NewGuid(), "askdfj", "");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestEmptyDisplay()
        {
            Action act = () => new ProfileAggregate(Guid.NewGuid(), "", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            act.Should().Throw<ArgumentException>();
        }
        
        [Fact]
        public void UserProfileTestExceedingLengthDisplay()
        {
            Action act = () => new ProfileAggregate(Guid.NewGuid(), "ASDFKLASLDKFASKLJDFFSADLJKFSADFKJASDJKLFSKALDJFFJKLASDFLKJSADFlkJASDFLKJASDFJKLASDFLJKASDLKJASFDJLK", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png");
            act.Should().Throw<ArgumentException>();
        }
    }
}