using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ShareRecipe.Services.KweetService.API.Application.Commands;
using ShareRecipe.Services.KweetService.API.Application.Commands.CreateKweet;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.API.Application.Validations
{
    public class CreateKweetCommandValidator : AbstractValidator<CreateKweetCommand>
    {
         private readonly IKweetRepository _kweetRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
        /// </summary>
        /// <param name="kweetRepository">The user repository.</param>
        public CreateKweetCommandValidator(IKweetRepository kweetRepository)
        {
            _kweetRepository = kweetRepository ?? throw new ArgumentNullException(nameof(kweetRepository));
            
            RuleFor(createUserCommand => createUserCommand.UserId)
                .CustomAsync(CheckUserIdExistsAsync);

            RuleFor(createUserCommand => createUserCommand.Message)
                .NotEmpty()
                .WithMessage("The profile picture url is null, empty or contains only white spaces.");
        }

        private async Task CheckUserIdExistsAsync(Guid proposedId, ValidationContext<CreateKweetCommand> context, CancellationToken cancellationToken)
        {
            // Checks whether the user id is empty.
            if (proposedId == Guid.Empty)
            {
                context.AddFailure("The user id can not be empty.");
                return;
            }
        }
    }
}