using System.Threading;
using System.Threading.Tasks;
using EasyNetQ.AutoSubscribe;

namespace ShareRecipe.Services.Common.API.Startup
{
    public class AutoSubscriberMessageDispatcher : IAutoSubscriberMessageDispatcher
    {
        public void Dispatch<TMessage, TConsumer>(TMessage message, CancellationToken cancellationToken = new CancellationToken()) where TMessage : class where TConsumer : class, IConsume<TMessage>
        {
            throw new System.NotImplementedException();
        }

        public Task DispatchAsync<TMessage, TConsumer>(TMessage message,
            CancellationToken cancellationToken = new CancellationToken()) where TMessage : class where TConsumer : class, IConsumeAsync<TMessage>
        {
            throw new System.NotImplementedException();
        }
    }
}