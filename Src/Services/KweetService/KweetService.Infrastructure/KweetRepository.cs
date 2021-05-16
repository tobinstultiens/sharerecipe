using System;
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

        public KweetAggregate Add(KweetAggregate userProfile)
        {
            return _context.Kweets.Add(userProfile).Entity;
        }

        public KweetAggregate Update(KweetAggregate userProfile)
        {
            return _context.Update(userProfile).Entity;
        }

        public async Task<KweetAggregate> GetAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Kweets.FindAsync(new object[]{userId}, cancellationToken);
        }

        public async Task<KweetAggregate> GetByMessageAsync(string message)
        {
            return await _context.Kweets.AsQueryable()
                .Where(user => string.Equals(user.Message, message, StringComparison.CurrentCultureIgnoreCase))
                .SingleOrDefaultAsync();
        }
    }
}
