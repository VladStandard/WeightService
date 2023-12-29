namespace Ws.WebApiScales.Dto.Plu
{
    public static partial class PluDtoMapper
    {
        public static Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p1, Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity result = p2 ?? new Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity();
            
            result.Weight = p1.PackageTypeWeight;
            result.Uid1C = p1.PackageTypeGuid;
            result.Name = p1.PackageTypeName;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p3, Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity p4)
        {
            if (p3 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity result = p4 ?? new Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity();
            
            result.IsGroup = p3.IsGroup;
            result.Number = (short)p3.PluNumber;
            result.Code = p3.Code;
            result.FullName = p3.FullName;
            result.ShelfLifeDays = (byte)p3.ShelfLife;
            result.Gtin = p3.IsCheckWeight == true ? "0" + p3.Ean13 : p3.Itf14;
            result.Ean13 = p3.Ean13;
            result.Itf14 = p3.IsCheckWeight == true ? "" : p3.Itf14;
            result.IsCheckWeight = p3.IsCheckWeight;
            result.Description = p3.Description;
            result.Name = p3.Name;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p5, Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity p6)
        {
            if (p5 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity result = p6 ?? new Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity();
            
            result.Weight = p5.BoxTypeWeight;
            result.Uid1C = p5.BoxTypeGuid;
            result.Name = p5.BoxTypeName;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p7, Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity p8)
        {
            if (p7 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity result = p8 ?? new Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity();
            
            result.Weight = p7.ClipTypeWeight;
            result.Uid1C = p7.ClipTypeGuid;
            result.Name = p7.ClipTypeName;
            return result;
            
        }
    }
}