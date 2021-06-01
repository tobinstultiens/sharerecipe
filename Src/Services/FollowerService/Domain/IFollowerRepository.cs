using System;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.FollowerService.Domain
{
    public interface IFollowerRepository : IRepository<ProfileAggregate>
    {
        ProfileAggregate Add(ProfileAggregate profileAggregate);
        Task<ProfileAggregate> Find(Guid userId);
    }
}