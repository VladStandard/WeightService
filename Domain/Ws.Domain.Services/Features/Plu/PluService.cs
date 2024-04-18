using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref1c.Plus;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plu;

internal class PluService(
    SqlPluRepository pluRepo,
    IRedisCachingProvider provider) : IPluService
{
    #region Queries

    [Transactional]
    public PluEntity GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<PluEntity> GetAll() => pluRepo.GetAll();

    public string GetPluCachedTemplate(PluEntity plu)
    {
        List<string> templatesKeys = provider.SearchKeys("TEMPLATE-*:PLUS");

        foreach (string templateKey in templatesKeys)
        {
            if (!provider.SIsMember(templateKey, $"{plu.Uid}"))
                continue;

            string? zpl = provider.StringGet($"{templateKey.Replace(":PLUS", ":ZPL")}");

            if (zpl != null)
                return zpl;
            break;
        }

        return "";
    }

    #endregion

    [Transactional]
    public PluEntity Update(PluEntity item) => pluRepo.Update(item);
}