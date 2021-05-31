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

        public ProfileAggregate Add(ProfileAggregate userProfile)
        {
            return _context.Profile.Add(userProfile).Entity;
        }

        public async Task<ProfileAggregate> Find(Guid userid)
        {
            return await _context.Profile.FindAsync(userid);
        }
    }
}