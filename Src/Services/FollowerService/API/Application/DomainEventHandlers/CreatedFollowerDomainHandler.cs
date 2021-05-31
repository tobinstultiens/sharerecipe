using System.Threading;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;
using MediatR;
using ShareRecipe.Services.Follower.Domain.Events;

namespace ShareRecipe.Services.Follower.API.Application.DomainEventHandlers
{
    public class CreatedFollowerDomainHandler : INotificationHandler<FollowerCreatedDomainEvent>
    {
        private readonly IBus _bus;

        public CreatedFollowerDomainHandler(IBus bus)
        {
            _bus = bus;
        }

        public Task Handle(FollowerCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            CreatedFollowerIntegrationEvent integrationEvent = new(notification.FollowerId, notification.FollowingId);
            _bus.PubSub.PublishAsync(integrationEvent, configuration => configuration.WithTopic("Follower.Created"), cancellationToken);
            //_bus.Publish(exchange, binding.RoutingKey, true, message, cancellationToken: cancellationToken);
            return Task.CompletedTask;
        }
    }
}