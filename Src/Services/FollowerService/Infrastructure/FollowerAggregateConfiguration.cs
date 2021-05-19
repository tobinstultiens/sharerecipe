using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.Infrastructure
{
    public class FollowerAggregateConfiguration: IEntityTypeConfiguration<FollowerAggregate>
    {
        public void Configure(EntityTypeBuilder<FollowerAggregate> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.FollowerId)
                .IsRequired(true);
            builder.Property(p => p.FollowedId)
                .IsRequired(true);
            builder.Property(p => p.FollowedAt);
        }
    }
}