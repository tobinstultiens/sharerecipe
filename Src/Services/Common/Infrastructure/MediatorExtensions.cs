using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ShareRecipe.Services.Common.Domain;
using ShareRecipe.Services.Common.Domain.Events;

namespace Infrastructure
{
    public static class MediatorExtensions
    {
        /// <summary>
        /// Dispatches the domain events asynchronously.
        /// </summary>
        /// <param name="mediator">The mediator.</param>
        /// <param name="dbContext">The database context.</param>
        /// <returns>Returns an awaitable task.</returns>
        public static async Task DispatchDomainEventsAsync(this IMediator mediator, DbContext dbContext)
        {
            IEnumerable<EntityEntry<Entity>> domainEntities = dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any());

            List<DomainEvent> domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (DomainEvent domainEvent in domainEvents)
                await mediator.Publish(domainEvent);
        }
    }
}