using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Plu;

public interface IPluService : IUid<PluEntity>, IUid1C<PluEntity>, IAll<PluEntity>
{
    public void DeleteAllPluNestings(PluEntity plu);
    public PluNestingEntity GetDefaultNesting(PluEntity plu);
    public IEnumerable<PluNestingEntity> GetAllPluNestings(PluEntity plu);
    PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public TemplateEntity GetPluTemplate(PluEntity plu);
    public PluLineEntity GetPluLineByPlu1СAndLineName(Guid pluUid, string lineName);
}