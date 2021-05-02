using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using ShareRecipe.Services.Common.API.CQRS;
using ShareRecipe.Services.ProfileService.Application.Application.Commands;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Application.Application.Validations
{
    public class CreateUserProfileCommandValidator : AbstractValidator<CreateUserProfileCommand>
    {
         private readonly IUserProfileRepository _userRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public CreateUserProfileCommandValidator(IUserProfileRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            
            RuleFor(createUserCommand => createUserCommand.UserDisplayName)
                .Custom(ValidateUserDisplayName);

            RuleFor(createUserCommand => createUserCommand.UserId)
                .CustomAsync(CheckUserIdUniquenessAsync);

            RuleFor(createUserCommand => createUserCommand.UserProfileImage)
                .NotEmpty()
                .WithMessage("The profile picture url is null, empty or contains only white spaces.");
            
            RuleFor(x => x.UserProfileImage).Must(uri => Uri.TryCreate(uri, UriKind.Absolute, out _)).When(x => !string.IsNullOrEmpty(x.UserProfileImage));
        }

        private void ValidateUserDisplayName(string displayName, ValidationContext<CreateUserProfileCommand> context)
        {
            // Checks whether the display name is not null, empty or contains only white spaces.
            if (string.IsNullOrWhiteSpace(displayName))
            {
                context.AddFailure("The display name must not be null or empty.");
                return;
            }

            // Checks whether the display name does not exceed 64 characters.
            if (displayName.Length > 30)
            {
                context.AddFailure("The display name must not exceed 64 characters.");
            }
        }

        private async Task CheckUserIdUniquenessAsync(Guid proposedUserId, ValidationContext<CreateUserProfileCommand> context, CancellationToken cancellationToken)
        {
            // Checks whether the user id is empty.
            if (proposedUserId == Guid.Empty)
            {
                context.AddFailure("The user id can not be empty.");
                return;
            }
            // Checks whether the user id already exists.
            UserProfileAggregate userAggregate = await _userRepository.GetAsync(proposedUserId, cancellationToken);
            if (userAggregate != default)
            {
                context.AddFailure("A user with the proposed user id already exists.");
                return;
            }
        }
    }
}