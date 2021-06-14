using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ShareRecipe.Services.Common.Domain.Persistence;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.Infrastructure
{
    public class KweetRepository : IKweetRepository
    {
        private readonly KweetContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public KweetRepository(KweetContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ProfileAggregate Add(ProfileAggregate userProfile)
        {
            return _context.Profile.Add(userProfile).Entity;
        }

        public async Task<ProfileAggregate> GetAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Profile.FindAsync(new object[] {userId}, cancellationToken);
        }

        public async Task<List<ProfileAggregate>> GetAllAsync(int size, int page, CancellationToken cancellationToken)
        {
            return await _context.Profile.Skip(size * (page - 1)).Take(size)
                .ToListAsync(cancellationToken: cancellationToken);
        }

        public async Task<Kweet> GetByMessageAsync(string message)
        {
            // return await _context.Profile.AsQueryable()
            // .Where(user => string.Equals(user.Kweets.Find(kweet => kweet.Message == message).Message, message, StringComparison.CurrentCultureIgnoreCase))
            // .SingleOrDefaultAsync();
            return null;
        }
    }
}