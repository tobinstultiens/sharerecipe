using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.Infrastructure.Configurations
{
    public class ProfileAggregateConfiguration : IEntityTypeConfiguration<ProfileAggregate>
    {
        public void Configure(EntityTypeBuilder<ProfileAggregate> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.DisplayName)
                .IsRequired(true);
            builder.Property(p => p.DisplayName)
                .HasMaxLength(30);
            builder.Property(p => p.ProfilePictureUrl)
                .IsRequired(true);

            builder.HasMany(p => p.Kweets)
                .WithOne()
                .HasForeignKey(p => p.Id)
                .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}