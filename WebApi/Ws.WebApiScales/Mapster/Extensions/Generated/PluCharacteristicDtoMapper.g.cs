using Ws.Domain.Models.Entities.Scale;

namespace Ws.WebApiScales.Features.Nesting.Dto
{
    public static partial class PluCharacteristicDtoMapper
    {
        public static PluNestingEntity AdaptTo(this Ws.WebApiScales.Features.Nesting.Dto.PluCharacteristicDto p1, PluNestingEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            PluNestingEntity result = p2 ?? new PluNestingEntity();
            
            result.BundleCount = (short)p1.AttachmentsCountAsInt;
            result.Uid1C = p1.Guid;
            return result;
            
        }
    }
}