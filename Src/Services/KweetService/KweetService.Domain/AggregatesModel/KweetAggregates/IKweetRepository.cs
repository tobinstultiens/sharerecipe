using System;
using System.Threading;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public interface IKweetRepository : IRepository<KweetAggregate>
    {
        KweetAggregate Add(KweetAggregate userProfile);
        KweetAggregate Update(KweetAggregate userProfile);
        Task<KweetAggregate> GetAsync(Guid userId, CancellationToken cancellationToken);
        Task<KweetAggregate> GetByMessageAsync(string message);
    }
}
