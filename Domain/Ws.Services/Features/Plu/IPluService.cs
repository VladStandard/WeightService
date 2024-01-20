using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;

namespace Ws.Services.Features.Plu;

public interface IPluService
{
    public IEnumerable<PluNestingEntity> GetPluNesting(PluEntity plu);
    public TemplateEntity GetPluTemplate(PluEntity plu);
    public PluLineEntity GetPluLineByPlu1СAndLineName(Guid pluUid, string lineName);
}