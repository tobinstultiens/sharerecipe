using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using ShareRecipe.Services.Follower.API.Application.Commands;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.API.Application.Validations
{
    public class CreatFollowerCommandValidator : AbstractValidator<CreateFollowerCommand>
    {
         private readonly IFollowerRepository _followerRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateUserCommandValidator"/> class.
        /// </summary>
        /// <param name="followerRepository">The user repository.</param>
        public CreatFollowerCommandValidator(IFollowerRepository followerRepository)
        {
            _followerRepository = followerRepository ?? throw new ArgumentNullException(nameof(followerRepository));
            
            RuleFor(createUserCommand => createUserCommand.FollowerId)
                .CustomAsync(CheckUserIdExistsAsync);
            RuleFor(createUserCommand => createUserCommand.FollowedId)
                .CustomAsync(CheckUserIdExistsAsync);
        }

        private async Task CheckUserIdExistsAsync(Guid proposedId, ValidationContext<CreateFollowerCommand> context, CancellationToken cancellationToken)
        {
            // Checks whether the user id is empty.
            if (proposedId == Guid.Empty)
            {
                context.AddFailure("The user id can not be empty.");
                return;
            }
            // Checks whether the user id already exists.
            // TODO Check if user ids already exists.
            //KweetAggregate kweetAggregate = await _kweetRepository.GetAsync(proposedId, cancellationToken);
            //if (kweetAggregate != default)
            //{
            //    context.AddFailure("A user with the proposed user id already exists.");
            //    return;
            //}
        }
    }
}