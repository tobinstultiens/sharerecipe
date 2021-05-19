using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.Infrastructure
{
    public class FollowerContext : UnitOfWork<FollowerContext>
    {
        public DbSet<FollowerAggregate> Followers { get; set; }

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
            modelBuilder.ApplyConfiguration(new FollowerAggregateConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
    }
}