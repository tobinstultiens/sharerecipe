using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class IUserRepository: IRepository<UserProfileAggregate>
    {
        public IUnitOfWork UnitOfWork { get; }
    }
}