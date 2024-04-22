using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Template.Validators;

namespace Ws.Domain.Services.Features.Template;

internal class TemplateService(SqlTemplateRepository templateRepo, IRedisCachingProvider provider) : ITemplateService
{
    [Transactional]
    public IEnumerable<TemplateEntity> GetAll() => templateRepo.GetAll();

    [Transactional]
    public TemplateEntity GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);

    [Transactional, Validate<TemplateNewValidator>]
    public TemplateEntity Create(TemplateEntity item) => templateRepo.Save(item);

    [Transactional, Validate<TemplateUpdateValidator>]
    public TemplateEntity Update(TemplateEntity item)
    {
        TemplateEntity template = templateRepo.Update(item);

        string zplKey = $"TEMPLATES:{template.Uid}";
        if (provider.KeyExists(zplKey))
            provider.StringSet(zplKey, template.Body, TimeSpan.FromHours(1));

        return template;
    }

    [Transactional]
    public void Delete(TemplateEntity item) => templateRepo.Delete(item);

    [Transactional]
    public IEnumerable<TemplateEntity> GetTemplatesByIsWeight(bool isWeight) =>
        templateRepo.GetTemplatesByIsWeight(isWeight);

    public string? GetTemplateByUidFromCacheOrDb(Guid templateUid)
    {
        string zplKey = $"TEMPLATES:{templateUid}";

        if (provider.KeyExists(zplKey))
            return provider.StringGet(zplKey);

        TemplateEntity temp = GetItemByUid(templateUid);

        if (!temp.IsExists || temp.Body == string.Empty) return null;

        provider.StringSet(zplKey, temp.Body, TimeSpan.FromHours(1));
        return temp.Body;
    }
}