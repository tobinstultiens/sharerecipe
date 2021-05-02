using System;
using System.Threading;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public interface IUserProfileRepository : IRepository<UserProfileAggregate>
    {
        UserProfileAggregate Add(UserProfileAggregate userProfile);
        UserProfileAggregate Update(UserProfileAggregate userProfile);
        Task<UserProfileAggregate> GetAsync(Guid userId, CancellationToken cancellationToken);
        Task<UserProfileAggregate> GetByDisplayNameAsync(string displayName);
    }
}