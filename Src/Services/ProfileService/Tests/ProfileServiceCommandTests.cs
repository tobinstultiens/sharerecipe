using System;
using System.Threading;
using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using ShareRecipe.Services.ProfileService.API.Application.Commands;
using ShareRecipe.Services.ProfileService.API.Application.Validations;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;
using Xunit;

namespace ShareRecipe.Services.ProfileService.Tests
{
    public class ProfileServiceCommandTests
    {
        private Guid _guid;

        public ProfileServiceCommandTests()
        {
            _guid = Guid.NewGuid();
        }

        [Fact]
        public void CorrectValidation()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.NewGuid(),
                UserDisplayName = "heyehey",
                UserProfileDescription = "hey",
                UserProfileImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileImage);
        }
        [Fact]
        public void EmptyDisplayName()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.NewGuid(),
                UserDisplayName = "",
                UserProfileDescription = "hey",
                UserProfileImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileImage);
        }
        [Fact]
        public void DisplayNameOverLimit()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.NewGuid(),
                UserDisplayName = "heyeheyaskldfasjkfjkasjkasdfjfklasdlfjkfasdkjfasdkjlfaslkdjfasjlkdsafdjkjaskldfjklsadf",
                UserProfileDescription = "hey",
                UserProfileImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileImage);
        }
        
        [Fact]
        public void ProfileImageEmpty()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.NewGuid(),
                UserDisplayName = "heyehey",
                UserProfileDescription = "hey",
                UserProfileImage = ""
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldHaveValidationErrorFor(x => x.UserProfileImage);
        }
        [Fact]
        public void InvalidImageUrl()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.NewGuid(),
                UserDisplayName = "heyehey",
                UserProfileDescription = "hey",
                UserProfileImage = "wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldNotHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldHaveValidationErrorFor(x => x.UserProfileImage);
        }
        [Fact]
        public void GuidCannotBeEmpty()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = Guid.Empty,
                UserDisplayName = "heyehey",
                UserProfileDescription = "hey",
                UserProfileImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileImage);
        }
        [Fact]
        public void GuidAlreadyExists()
        {
            var userprofile = new Mock<IUserProfileRepository>();
            userprofile.Setup(repository => repository.GetAsync(_guid, CancellationToken.None)).ReturnsAsync(new UserProfileAggregate(Guid.NewGuid(), "sup","hey", "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"));
            
            var validator = new CreateUserProfileCommandValidator(userprofile.Object);

            CreateUserProfileCommand model = new CreateUserProfileCommand
            {
                UserId = _guid,
                UserDisplayName = "sup",
                UserProfileDescription = "hey",
                UserProfileImage = "https://upload.wikimedia.org/wikipedia/commons/thumb/b/b6/Image_created_with_a_mobile_phone.png/1200px-Image_created_with_a_mobile_phone.png"
            };
            var result = validator.TestValidate(model);
            result.ShouldHaveValidationErrorFor(x => x.UserId);
            result.ShouldNotHaveValidationErrorFor(x => x.UserDisplayName);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileDescription);
            result.ShouldNotHaveValidationErrorFor(x => x.UserProfileImage);
        }
    }
}