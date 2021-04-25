using System;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        public IUnitOfWork UnitOfWork { get; }
        
        public UserProfileAggregate Create(UserProfileAggregate profile)
        {
            throw new System.NotImplementedException();
        }

        public UserProfileAggregate Update(UserProfileAggregate profile)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfileAggregate> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}