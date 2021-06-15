using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShareRecipe.Services.Common.Infrastructure;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;
using ShareRecipe.Services.KweetService.Infrastructure.Configurations;

namespace ShareRecipe.Services.KweetService.Infrastructure
{
    public class KweetContext : UnitOfWork<KweetContext>
    {
        public DbSet<ProfileAggregate> Profile { get; set; }
        public DbSet<Kweet> Kweets { get; set; }

        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
        }

        public KweetContext(DbContextOptions<KweetContext> options, IMediator mediator,
            ILogger<KweetContext> createLogger) : base(options,
            mediator, createLogger)
        {
        }
        
        /// <summary>
        /// Applies the entity configurations during the build of the model.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KweetConfiguration());
            modelBuilder.ApplyConfiguration(new ProfileAggregateConfiguration());
            base.OnModelCreating(modelBuilder);
        } 
    }
}
