using Ws.WebApiScales.Features.Characteristics.Dto;

namespace Ws.WebApiScales.Features.Nesting.Dto
{
    public static partial class PluCharacteristicDtoMapper
    {
        public static Ws.Domain.Models.Entities.Scale.PluNestingEntity AdaptTo(this CharacteristicDto p1, Ws.Domain.Models.Entities.Scale.PluNestingEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Scale.PluNestingEntity result = p2 ?? new Ws.Domain.Models.Entities.Scale.PluNestingEntity();
            
            result.BundleCount = (short)p1.BundleCount;
            result.Uid1C = p1.Uid;
            return result;
            
        }
    }
}