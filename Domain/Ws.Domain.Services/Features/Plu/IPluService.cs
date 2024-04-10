using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Common.Queries;

namespace Ws.Domain.Services.Features.Plu;

public interface IPluService : IGetItemByUid<PluEntity>, IGetAll<PluEntity>
{
    #region Queries

    public TemplateEntity GetPluTemplate(PluEntity plu);
    public string GetPluCachedTemplate(PluEntity plu);

    #endregion
}