using Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class UserProfileContext : UnitOfWork<UserProfileContext>
    {
        public DbSet<UserProfileAggregate> UserProfiles { get; set; }

        public UserProfileContext(DbContextOptions<UserProfileContext> options) : base(options)
        {
        }

        public UserProfileContext(DbContextOptions<UserProfileContext> options, IMediator mediator) : base(options,
            mediator)
        {
        }
        
        /// <summary>
        /// Applies the entity configurations during the build of the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserProfileAggregateConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
    }
}