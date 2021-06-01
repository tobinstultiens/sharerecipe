using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.FollowerService.Domain;
using ShareRecipe.Services.FollowerService.Infrastructure.Configurations;

namespace ShareRecipe.Services.FollowerService.Infrastructure
{
    public class FollowerContext : UnitOfWork<FollowerContext>
    {
        public DbSet<ProfileAggregate> Profile { get; set; }

        public FollowerContext(DbContextOptions<FollowerContext> options) : base(options)
        {
        }

        public FollowerContext(DbContextOptions<FollowerContext> options, IMediator mediator,
            ILogger<FollowerContext> createLogger) : base(options,
            mediator, createLogger)
        {
        }
        
        /// <summary>
        /// Applies the entity configurations during the build of the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProfileAggregateConfiguration());
            modelBuilder.ApplyConfiguration(new FollowerConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
    }
}