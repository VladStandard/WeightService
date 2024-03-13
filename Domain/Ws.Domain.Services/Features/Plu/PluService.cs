using EasyCaching.Core;
using Ws.Database.Core.Entities.Ref1c.Plus;
using Ws.Database.Core.Entities.Scales.PlusNestingFks;
using Ws.Database.Core.Entities.Scales.PlusTemplatesFks;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Plu;

internal class PluService(
    SqlPluRepository pluRepo,
    SqlPluNestingFkRepository pluNestingFkRepo,
    SqlPluTemplateFkRepository pluTemplateFkRepo,
    IRedisCachingProvider provider) : IPluService
{
    #region Queries

    [Transactional]
    public PluEntity GetItemByUid(Guid uid) => pluRepo.GetByUid(uid);

    [Transactional]
    public PluEntity GetItemByUid1С(Guid uid) => pluRepo.GetByUid1C(uid);

    [Transactional]
    public IEnumerable<PluEntity> GetAll() => pluRepo.GetAll();

    [Transactional]
    public IEnumerable<PluNestingEntity> GetAllPluNestings(PluEntity plu) => pluNestingFkRepo.GetAllByPlu(plu);

    [Transactional]
    public PluNestingEntity GetDefaultNesting(PluEntity plu) => pluNestingFkRepo.GetDefaultByPlu(plu);

    [Transactional]
    public PluNestingEntity GetNestingByUid1C(PluEntity plu, Guid nestingUid1C) =>
        pluNestingFkRepo.GetByPluAndUid1C(plu, nestingUid1C);

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

    #region Commands

    [Transactional]
    public void DeleteAllPluNestings(PluEntity plu) => pluNestingFkRepo.DeleteAllPluNestings(plu);

    [Transactional]
    public void DeleteNestingByUid1C(PluEntity plu, Guid nestingUid1C)
    {
        PluNestingEntity nesting = GetNestingByUid1C(plu, nestingUid1C);
        if (nesting.IsExists) pluNestingFkRepo.Delete(nesting);
    }

    #endregion
}