using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.WebApiScales.Features.Plu.Dto
{
    public static partial class PluDtoMapper
    {
        public static BoxEntity AdaptTo(this Ws.WebApiScales.Features.Plu.Dto.PluDto p1, BoxEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            BoxEntity result = p2 ?? new BoxEntity();
            
            result.Weight = p1.BoxTypeWeight;
            result.Uid1C = p1.BoxTypeGuid;
            result.Name = p1.BoxTypeName;
            return result;
            
        }
        public static PluEntity AdaptTo(this Ws.WebApiScales.Features.Plu.Dto.PluDto p3, PluEntity p4)
        {
            if (p3 == null)
            {
                return null;
            }
            PluEntity result = p4 ?? new PluEntity();
            
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
        public static BundleEntity AdaptTo(this Ws.WebApiScales.Features.Plu.Dto.PluDto p5, BundleEntity p6)
        {
            if (p5 == null)
            {
                return null;
            }
            BundleEntity result = p6 ?? new BundleEntity();
            
            result.Weight = p5.PackageTypeWeight;
            result.Uid1C = p5.PackageTypeGuid;
            result.Name = p5.PackageTypeName;
            return result;
            
        }
        public static ClipEntity AdaptTo(this Ws.WebApiScales.Features.Plu.Dto.PluDto p7, ClipEntity p8)
        {
            if (p7 == null)
            {
                return null;
            }
            ClipEntity result = p8 ?? new ClipEntity();
            
            result.Weight = p7.ClipTypeWeight;
            result.Uid1C = p7.ClipTypeGuid;
            result.Name = p7.ClipTypeName;
            return result;
            
        }
    }
}