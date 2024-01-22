using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Services.Common;

namespace Ws.Services.Features.Plu;

public interface IPluService : IUid<PluEntity>, IUid1C<PluEntity>
{
    public IEnumerable<PluEntity> GetAllNotGroup();
    public IEnumerable<PluNestingEntity> GetPluNestings(PluEntity plu);
    public PluNestingEntity GetDefaultNesting(PluEntity plu);
    PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public TemplateEntity GetPluTemplate(PluEntity plu);
    public PluLineEntity GetPluLineByPlu1СAndLineName(Guid pluUid, string lineName);
    PluFkEntity GetParent(PluEntity plu);
    IEnumerable<PluEntity> GetInRange(List<Guid> uniquePluGuids);

}