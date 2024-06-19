using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

internal class CharacteristicMapConfig : IEntityTypeConfiguration<CharacteristicEntity>
{
    public void Configure(EntityTypeBuilder<CharacteristicEntity> builder)
    {
        builder.HasOne<PluEntity>()
            .WithMany()
            .HasForeignKey(characteristic => characteristic.PluId)
            .HasConstraintName("FK_CHARACTERISTICS_PLUS_PLU_UID");
    }
}