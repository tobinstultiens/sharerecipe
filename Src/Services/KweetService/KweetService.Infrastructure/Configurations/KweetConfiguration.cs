using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Services.KweetService.Domain.AggregatesModel.KweetAggregates;

namespace ShareRecipe.Services.KweetService.Infrastructure.Configurations
{
    public class KweetConfiguration: IEntityTypeConfiguration<Kweet>
    {
        public void Configure(EntityTypeBuilder<Kweet> builder)
        {
            builder.Ignore(p => p.DomainEvents);
            builder.HasKey(p => p.Id);
            builder.Property(p => p.UserId)
                .IsRequired(true);
            builder.Property(p => p.Message)
                .IsRequired(true);
            builder.OwnsMany(p => p.Directions, navigationBuilder =>
            {
                navigationBuilder.Ignore(p => p.DomainEvents);
                navigationBuilder.Ignore(p => p.Id);
                navigationBuilder.Property(p => p.Message)
                    .IsRequired(true);
                navigationBuilder.Property(p => p.Order)
                    .HasMaxLength(50);
                navigationBuilder.WithOwner();
            });

            builder.OwnsMany<Ingredient>(p => p.Ingredients, navigationBuilder =>
            {
                navigationBuilder.Ignore(p => p.DomainEvents);
                navigationBuilder.Ignore(p => p.Id);
                navigationBuilder.Property(p => p.Description)
                    .HasMaxLength(100);
                navigationBuilder.Property(p => p.Description)
                    .IsRequired(true);
                navigationBuilder.Property(p => p.Amount)
                    .HasMaxLength(50);
                navigationBuilder.WithOwner();
            });
        }
    }
}
