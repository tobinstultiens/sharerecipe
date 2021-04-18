using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates
{
    public interface IUserProfileRepository : IRepository<UserProfile>
    {
        UserProfile Add(UserProfile order);
        void Update(UserProfile order);
        Task<UserProfile> GetAsync(int orderId);
    }
}