using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShareRecipe.Services.Common.Infrastructure;

namespace ShareRecipe.Services.FollowerService.Infrastructure
{
    public sealed class FollowerDatabaseFactory : DatabaseFactory<FollowerContext>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowerDatabaseFactory"/> class.
        /// </summary>
        public FollowerDatabaseFactory() : base("Service", (IOptions<DbConfiguration>) null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FollowerDatabaseFactory"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="mediator">The mediator.</param>
        public FollowerDatabaseFactory(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory, IMediator mediator) : base("Service", dbConfigurationOptions, loggerFactory)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The options.</param>
        /// <returns>The user database context.</returns>
        protected override FollowerContext CreateNewInstance(DbContextOptions<FollowerContext> dbContextOptions)
        {
            return LoggerFactory is null
                ? (new(dbContextOptions, _mediator, default))
                : (new(dbContextOptions, _mediator, LoggerFactory.CreateLogger<FollowerContext>()));
        }
    }
}