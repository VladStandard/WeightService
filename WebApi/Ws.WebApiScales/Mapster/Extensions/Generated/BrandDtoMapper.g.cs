using Ws.Domain.Models.Entities.Ref1c;

namespace Ws.WebApiScales.Features.Brand.Dto
{
    public static partial class BrandDtoMapper
    {
        public static BrandEntity AdaptTo(this Ws.WebApiScales.Features.Brand.Dto.BrandDto p1, BrandEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            BrandEntity result = p2 ?? new BrandEntity();
            
            result.Code = p1.Code;
            result.Uid1C = p1.Guid;
            result.Name = p1.Name;
            return result;
            
        }
    }
}