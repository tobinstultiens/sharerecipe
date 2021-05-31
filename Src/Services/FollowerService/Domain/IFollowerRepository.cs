using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.Follower.Domain
{
    public interface IFollowerRepository : IRepository<ProfileAggregate>
    {
        ProfileAggregate Add(ProfileAggregate profileAggregate);
        Task<ProfileAggregate> Find(Guid userId);
    }
}