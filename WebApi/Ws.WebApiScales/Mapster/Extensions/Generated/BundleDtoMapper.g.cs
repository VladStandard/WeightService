namespace Ws.WebApiScales.Features.Bundles.Dto
{
    public static partial class BundleDtoMapper
    {
        public static Ws.Domain.Models.Entities.Ref1c.BundleEntity AdaptTo(this Ws.WebApiScales.Features.Bundles.Dto.BundleDto p1, Ws.Domain.Models.Entities.Ref1c.BundleEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Ref1c.BundleEntity result = p2 ?? new Ws.Domain.Models.Entities.Ref1c.BundleEntity();
            
            result.Weight = p1.Weight;
            result.Uid1C = p1.Uid;
            result.Name = p1.Name;
            return result;
            
        }
    }
}