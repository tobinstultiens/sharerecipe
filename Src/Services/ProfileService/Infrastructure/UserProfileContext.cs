using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class UserProfileContext : UnitOfWork<UserProfileContext>
    {
        public DbSet<UserProfileAggregate> UserProfiles { get; set; }

        public UserProfileContext(DbContextOptions<UserProfileContext> options) : base(options)
        {
        }

        public UserProfileContext(DbContextOptions<UserProfileContext> options, IMediator mediator,
            ILogger<UserProfileContext> createLogger) : base(options,
            mediator, createLogger)
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