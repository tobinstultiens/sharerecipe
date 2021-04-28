using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.ProfileService.Domain.AggregatesModel.UserAggregates;

namespace ShareRecipe.Services.ProfileService.Infrastructure
{
    public class UserProfileAggregateConfiguration: IEntityTypeConfiguration<UserProfileAggregate>
    {
        public void Configure(EntityTypeBuilder<UserProfileAggregate> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DisplayName)
                .IsRequired(true);
            builder.Property(p => p.DisplayName)
                .HasMaxLength(30);

            builder.OwnsOne(p => p.UserProfile, navigationBuilder =>
            {
                navigationBuilder.Ignore(p => p.DomainEvents);
                navigationBuilder.Ignore(p => p.Id);
                navigationBuilder.Property(p => p.Description)
                    .HasMaxLength(300);
                navigationBuilder.Property(p => p.Description)
                    .IsRequired(true);
                navigationBuilder.Property(p => p.Image)
                    .HasMaxLength(512);
                navigationBuilder.Property(p => p.Image)
                    .IsRequired(true);
                navigationBuilder.WithOwner();
            });
        }
    }
}