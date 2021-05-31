using System;
using System.Threading;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates
{
    public interface IKweetRepository : IRepository<ProfileAggregate>
    {
        ProfileAggregate Add(ProfileAggregate userProfile);
        Task<ProfileAggregate> GetAsync(Guid userId, CancellationToken cancellationToken);
        Task<Kweet> GetByMessageAsync(string message);
    }
}
