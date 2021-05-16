using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ShareRecipe.Services.Common.Infrastructure;

namespace ShareRecipe.Services.KweetService.Infrastructure
{
    public sealed class KweetDatabaseFactory : DatabaseFactory<KweetContext>
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDatabaseFactory"/> class.
        /// </summary>
        public KweetDatabaseFactory() : base("Service", (IOptions<DbConfiguration>) null, null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserProfileDatabaseFactory"/> class.
        /// </summary>
        /// <param name="dbConfigurationOptions">The options.</param>
        /// <param name="loggerFactory">The logger factory.</param>
        /// <param name="mediator">The mediator.</param>
        public KweetDatabaseFactory(IOptions<DbConfiguration> dbConfigurationOptions, ILoggerFactory loggerFactory, IMediator mediator) : base("Service", dbConfigurationOptions, loggerFactory)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="UserDbContext"/> class.
        /// </summary>
        /// <param name="dbContextOptions">The options.</param>
        /// <returns>The user database context.</returns>
        protected override KweetContext CreateNewInstance(DbContextOptions<KweetContext> dbContextOptions)
        {
            return LoggerFactory is null
                ? (new(dbContextOptions, _mediator, default))
                : (new(dbContextOptions, _mediator, LoggerFactory.CreateLogger<KweetContext>()));
        }
    }
}
