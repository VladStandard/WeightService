using Ws.Database.EntityFramework.Entities.Ref1C.Plus;

namespace Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

internal static class CharacteristicMapExtension
{
    public static void MapCharacteristic(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacteristicEntity>(entity =>
        {
            entity
                .HasOne<PluEntity>()
                .WithMany()
                .HasForeignKey(characteristic => characteristic.PluId)
                .HasConstraintName("FK_CHARACTERISTICS_PLUS_PLU_UID");
        });
    }
}