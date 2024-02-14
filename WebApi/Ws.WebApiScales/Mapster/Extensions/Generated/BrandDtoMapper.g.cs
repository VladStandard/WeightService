namespace Ws.WebApiScales.Features.Brands.Dto
{
    public static partial class BrandDtoMapper
    {
        public static Ws.Domain.Models.Entities.Ref1c.BrandEntity AdaptTo(this Ws.WebApiScales.Features.Brands.Dto.BrandDto p1, Ws.Domain.Models.Entities.Ref1c.BrandEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Ref1c.BrandEntity result = p2 ?? new Ws.Domain.Models.Entities.Ref1c.BrandEntity();
            
            result.Name = p1.Name;
            result.Uid1C = p1.Uid;
            return result;
            
        }
    }
}