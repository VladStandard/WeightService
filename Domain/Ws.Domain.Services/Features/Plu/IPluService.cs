using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Plu;

public interface IPluService : IGetItemByUid<PluEntity>, IGetItemByUid1C<PluEntity>, IGetAll<PluEntity>
{
    public void DeleteAllPluNestings(PluEntity plu);
    public PluNestingEntity GetDefaultNesting(PluEntity plu);
    public IEnumerable<PluNestingEntity> GetAllPluNestings(PluEntity plu);
    PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C);
    public TemplateEntity GetPluTemplate(PluEntity plu);
}