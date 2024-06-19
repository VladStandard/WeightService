using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Ref.Claims;

namespace Ws.Database.EntityFramework.Entities.Ref.Users;

internal class UserMapConfig : IEntityTypeConfiguration<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.HasMany(e => e.Claims)
            .WithMany(e => e.Users)
            .UsingEntity(
                "USERS_CLAIMS_FK",
                l => l.HasOne(typeof(ClaimEntity))
                    .WithMany()
                    .HasForeignKey("CLAIM_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(ClaimEntity.Id)),
                r => r.HasOne(typeof(UserEntity))
                    .WithMany()
                    .HasForeignKey("USER_UID")
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasPrincipalKey(nameof(UserEntity.Id)),
                j => j.HasKey("CLAIM_UID", "USER_UID"));
    }
}