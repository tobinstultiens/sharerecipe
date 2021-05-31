using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ShareRecipe.Services.Follower.Infrastructure.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Domain.Follower>
    {
        public void Configure(EntityTypeBuilder<Domain.Follower> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.Ignore(p => p.Id);
            builder.HasKey(p => new {p.FollowerId, p.FollowingId});
            
            builder.Property(p => p.FollowedAt)
                .IsRequired();
        }
    }
}