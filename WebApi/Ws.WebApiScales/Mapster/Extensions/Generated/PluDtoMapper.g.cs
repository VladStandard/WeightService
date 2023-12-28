namespace Ws.WebApiScales.Dto.Plu
{
    public static partial class PluDtoMapper
    {
        public static Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p1, Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity result = p2 ?? new Ws.StorageCore.Entities.SchemaRef1c.Boxes.SqlBoxEntity();
            
            result.Weight = p1.BoxTypeWeight;
            result.Uid1C = p1.BoxTypeGuid;
            result.Name = p1.BoxTypeName;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p3, Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity p4)
        {
            if (p3 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity result = p4 ?? new Ws.StorageCore.Entities.SchemaRef1c.Clips.SqlClipEntity();
            
            result.Weight = p3.ClipTypeWeight;
            result.Uid1C = p3.ClipTypeGuid;
            result.Name = p3.ClipTypeName;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p5, Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity p6)
        {
            if (p5 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity result = p6 ?? new Ws.StorageCore.Entities.SchemaRef1c.Plus.SqlPluEntity();
            
            result.IsGroup = p5.IsGroup;
            result.Number = (short)p5.PluNumber;
            result.Code = p5.Code;
            result.FullName = p5.FullName;
            result.ShelfLifeDays = (byte)p5.ShelfLife;
            result.Gtin = p5.IsCheckWeight == true ? "0" + p5.Ean13 : p5.Itf14;
            result.Ean13 = p5.Ean13;
            result.Itf14 = p5.IsCheckWeight == true ? "" : p5.Itf14;
            result.IsCheckWeight = p5.IsCheckWeight;
            result.Description = p5.Description;
            result.IsMarked = p5.IsMarked;
            result.Name = p5.Name;
            return result;
            
        }
        public static Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity AdaptTo(this Ws.WebApiScales.Dto.Plu.PluDto p7, Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity p8)
        {
            if (p7 == null)
            {
                return null;
            }
            Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity result = p8 ?? new Ws.StorageCore.Entities.SchemaRef1c.Bundles.SqlBundleEntity();
            
            result.Weight = p7.PackageTypeWeight;
            result.Uid1C = p7.PackageTypeGuid;
            result.Name = p7.PackageTypeName;
            return result;
            
        }
    }
}