namespace Ws.WebApiScales.Dto.PluCharacteristic
{
    public static partial class PluCharacteristicDtoMapper
    {
        public static Ws.StorageCore.Entities.SchemaScale.PlusNestingFks.SqlPluNestingFkEntity AdaptTo(this Ws.WebApiScales.Dto.PluCharacteristic.PluCharacteristicDto p1, Ws.StorageCore.Entities.SchemaScale.PlusNestingFks.SqlPluNestingFkEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaScale.PlusNestingFks.SqlPluNestingFkEntity result = p2 ?? new Ws.StorageCore.Entities.SchemaScale.PlusNestingFks.SqlPluNestingFkEntity();
            
            result.BundleCount = (short)p1.AttachmentsCountAsInt;
            result.Uid1C = p1.Guid;
            result.IsMarked = p1.IsMarked;
            return result;
        }
    }
}