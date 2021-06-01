using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.FollowerService.Domain;

namespace ShareRecipe.Services.FollowerService.Infrastructure.Configurations
{
    public class ProfileAggregateConfiguration: IEntityTypeConfiguration<ProfileAggregate>
    {
        public void Configure(EntityTypeBuilder<ProfileAggregate> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DisplayName)
                .IsRequired(true);
            builder.Property(p => p.ProfilePictureUrl)
                .IsRequired(true);
            
            builder.HasMany(p => p.Followers)
                .WithOne()
                .HasForeignKey(p => p.FollowerId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasMany(p => p.Following)
                .WithOne()
                .HasForeignKey(p => p.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}