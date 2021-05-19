using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ShareRecipe.Services.Common.Domain.Persistence;

namespace ShareRecipe.Services.Follower.Domain
{
    public interface IFollowerRepository : IRepository<FollowerAggregate>
    {
        FollowerAggregate Add(FollowerAggregate followerAggregate);
        FollowerAggregate Remove(Guid followerId, Guid followedId);
        Task<List<FollowerAggregate>> GetAllFollowers(Guid followerId);
    }
}