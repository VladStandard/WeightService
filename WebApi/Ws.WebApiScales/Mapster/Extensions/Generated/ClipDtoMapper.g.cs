namespace Ws.WebApiScales.Features.Clips.Dto
{
    public static partial class ClipDtoMapper
    {
        public static Ws.Domain.Models.Entities.Ref1c.ClipEntity AdaptTo(this Ws.WebApiScales.Features.Clips.Dto.ClipDto p1, Ws.Domain.Models.Entities.Ref1c.ClipEntity p2)
        {
            if (p1 == null)
            {
                return null;
            }
            Ws.Domain.Models.Entities.Ref1c.ClipEntity result = p2 ?? new Ws.Domain.Models.Entities.Ref1c.ClipEntity();
            
            result.Weight = p1.Weight;
            result.Uid1C = p1.Uid;
            result.Name = p1.Name;
            return result;
            
        }
    }
}