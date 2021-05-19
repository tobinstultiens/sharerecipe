using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.Infrastructure
{
    public class FollowerRepository : IFollowerRepository
    {
        private readonly FollowerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public FollowerRepository(FollowerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public FollowerAggregate Add(FollowerAggregate userProfile)
        {
            return _context.Followers.Add(userProfile).Entity;
        }

        public FollowerAggregate Remove(Guid followerId, Guid followedId)
        {
            var FollowerAggregate = new FollowerAggregate(followerId, followedId, DateTime.Now);
            return _context.Followers.Remove(FollowerAggregate).Entity;
        }

        public async Task<List<FollowerAggregate>> GetAllFollowers(Guid followerId)
        {
            return await _context.Followers.Where(x => x.FollowerId == followerId).ToListAsync();
        }
    }
}