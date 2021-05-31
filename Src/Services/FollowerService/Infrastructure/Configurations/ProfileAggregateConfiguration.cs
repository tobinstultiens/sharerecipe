using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.Follower.Domain;

namespace ShareRecipe.Services.Follower.Infrastructure.Configurations
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
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .Navigation(p => p.Followers)
                .HasField("followers")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            builder.HasMany(p => p.Following)
                .WithOne()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder
                .Navigation(p => p.Following)
                .HasField("following")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}