using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.FollowerService.Domain;

namespace ShareRecipe.Services.FollowerService.Infrastructure.Configurations
{
    public class FollowerConfiguration : IEntityTypeConfiguration<Follower>
    {
        public void Configure(EntityTypeBuilder<Follower> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.Ignore(p => p.Id);
            builder.HasKey(p => new {p.FollowerId, p.FollowingId});
            builder.Property(p => p.FollowerId)
                .IsRequired(true);
            builder.Property(p => p.FollowingId)
                .IsRequired(true);
            
            builder.Property(p => p.FollowedAt)
                .IsRequired();
        }
    }
}