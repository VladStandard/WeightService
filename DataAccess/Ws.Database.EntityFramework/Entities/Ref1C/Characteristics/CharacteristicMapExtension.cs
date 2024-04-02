namespace Ws.Database.EntityFramework.Entities.Ref1C.Characteristics;

internal static class CharacteristicMapExtension
{
    public static void MapCharacteristic(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacteristicEntity>(entity =>
        {
            entity.HasIndex(pn => new { pn.PluEntityId, pn.IsDefault })
                .IsUnique()
                .HasDatabaseName($"UQ_{SqlTables.Characteristics}_IS_DEFAULT_TRUE_ON_PLU")
                .HasFilter("[IS_DEFAULT] = 1");
        });
    }
}