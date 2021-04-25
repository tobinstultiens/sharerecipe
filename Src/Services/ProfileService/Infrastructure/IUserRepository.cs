using System;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public interface IUserRepository: IRepository<UserProfileAggregate>
    {
        UserProfileAggregate Create(UserProfileAggregate profile);
        UserProfileAggregate Update(UserProfileAggregate profile);
        Task<UserProfileAggregate> FindByIdAsync(Guid id);
    }
}