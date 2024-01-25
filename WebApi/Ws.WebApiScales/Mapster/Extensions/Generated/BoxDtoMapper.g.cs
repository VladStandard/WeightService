using Ws.WebApiScales.Features.Boxes.Dto;

namespace Ws.WebApiScales.Features.Box.Dto
{
    public static partial class BoxDtoMapper
    {
        public static Ws.Domain.Models.Entities.Ref1c.BoxEntity AdaptTo(this BoxDto p1, Ws.Domain.Models.Entities.Ref1c.BoxEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Ref1c.BoxEntity result = p2 ?? new Ws.Domain.Models.Entities.Ref1c.BoxEntity();
            
            result.Weight = p1.Weight;
            result.Uid1C = p1.Uid;
            result.Name = p1.Name;
            return result;
            
        }
    }
}