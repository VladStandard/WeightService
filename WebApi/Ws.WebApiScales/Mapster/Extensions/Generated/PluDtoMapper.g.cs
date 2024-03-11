namespace Ws.WebApiScales.Features.Plus.Dto
{
    public static partial class PluDtoMapper
    {
        public static Ws.Domain.Models.Entities.Ref1c.PluEntity AdaptTo(this Ws.WebApiScales.Features.Plus.Dto.PluDto p1, Ws.Domain.Models.Entities.Ref1c.PluEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Ref1c.PluEntity result = p2 ?? new Ws.Domain.Models.Entities.Ref1c.PluEntity();
            
            result.Number = (short)p1.Number;
            result.FullName = p1.FullName;
            result.ShelfLifeDays = (byte)p1.ShelfLifeDays;
            result.Ean13 = p1.Ean13;
            result.Itf14 = p1.IsCheckWeight == true ? "" : p1.Itf14;
            result.IsCheckWeight = p1.IsCheckWeight;
            result.Description = p1.Description;
            result.Name = p1.Name;
            result.Uid1C = p1.Uid;
            return result;
            
        }
    }
}