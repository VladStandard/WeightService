using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref1c.Characteristics;
using Ws.Database.Nhibernate.Entities.Ref1c.Plus;
using Ws.Database.Nhibernate.Entities.Scales.PlusTemplatesFks;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plu;

internal class PluService(
    SqlPluRepository pluRepo,
    SqlPluTemplateFkRepository pluTemplateFkRepo,
    IRedisCachingProvider provider) : IPluService
{
    #region Queries

    [Transactional]
    public PluEntity GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    [Transactional]
    public IEnumerable<PluEntity> GetAll() => pluRepo.GetAll();

    [Transactional]
    public TemplateEntity GetPluTemplate(PluEntity plu) => pluTemplateFkRepo.GetTemplateByPlu(plu);

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

        TemplateEntity template = GetPluTemplate(plu);

        provider.SAdd($"TEMPLATE-{template.Uid}:PLUS", [$"{plu.Uid}"], TimeSpan.FromHours(1));
        provider.StringSet($"TEMPLATE-{template.Uid}:ZPL", template.Body, TimeSpan.FromHours(1));

        return template.Body;
    }

    #endregion
}