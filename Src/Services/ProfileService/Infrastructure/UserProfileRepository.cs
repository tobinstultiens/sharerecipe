using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly UserProfileContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public UserProfileAggregate Add(UserProfileAggregate userProfile)
        {
            return _context.UserProfiles.Add(userProfile).Entity;
        }

        public UserProfileAggregate Update(UserProfileAggregate userProfile)
        {
            return _context.Update(userProfile).Entity;
        }

        public async Task<UserProfileAggregate> GetAsync(Guid userId)
        {
            return await _context.UserProfiles.FindAsync(userId);
        }

        public async Task<UserProfileAggregate> GetByDisplayNameAsync(string displayName)
        {
            return await _context.UserProfiles.AsQueryable()
                .Where(user => string.Equals(user.DisplayName, displayName, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefaultAsync();
        }
    }
}