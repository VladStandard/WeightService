using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Templates.Specs;
using Ws.Domain.Services.Features.Templates.Validators;

namespace Ws.Domain.Services.Features.Templates;

internal class TemplateService(SqlTemplateRepository templateRepo, IRedisCachingProvider provider) : ITemplateService
{
    #region List

    [Transactional]
    public IList<Template> GetAll() => templateRepo.GetAll();

    [Transactional]
    public IList<Template> GetTemplatesByIsWeight(bool isWeight) =>
        templateRepo.GetListBySpec(isWeight ? TemplateSpec.GetForWeight : TemplateSpec.GetForPiece);

    #endregion

    #region Items

    [Transactional]
    public Template GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);

    #endregion

    #region CRUD

    [Transactional, Validate<TemplateNewValidator>]
    public Template Create(Template item) => templateRepo.Save(item);

    [Transactional, Validate<TemplateUpdateValidator>]
    public Template Update(Template item)
    {
        Template template = templateRepo.Update(item);

        string zplKey = $"TEMPLATES:{template.Uid}";
        if (provider.KeyExists(zplKey))
            provider.StringSet(zplKey, template.Body, TimeSpan.FromHours(1));

        return template;
    }

    [Transactional]
    public void Delete(Template item) => templateRepo.Delete(item);

    #endregion

    public string? GetTemplateByUidFromCacheOrDb(Guid templateUid)
    {
        string zplKey = $"TEMPLATES:{templateUid}";

        if (provider.KeyExists(zplKey))
            return provider.StringGet(zplKey);

        Template temp = GetItemByUid(templateUid);

        if (!temp.IsExists || temp.Body == string.Empty) return null;

        provider.StringSet(zplKey, temp.Body, TimeSpan.FromHours(1));
        return temp.Body;
    }
}