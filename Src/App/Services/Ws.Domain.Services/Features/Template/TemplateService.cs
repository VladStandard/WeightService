using EasyCaching.Core;
using Ws.Database.Nhibernate.Entities.Ref.Templates;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Template.Validators;

namespace Ws.Domain.Services.Features.Template;

internal class TemplateService(SqlTemplateRepository templateRepo, IRedisCachingProvider provider) : ITemplateService
{
    [Transactional]
    public IEnumerable<Models.Entities.Print.Template> GetAll() => templateRepo.GetAll();

    [Transactional]
    public Models.Entities.Print.Template GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);

    [Transactional, Validate<TemplateNewValidator>]
    public Models.Entities.Print.Template Create(Models.Entities.Print.Template item) => templateRepo.Save(item);

    [Transactional, Validate<TemplateUpdateValidator>]
    public Models.Entities.Print.Template Update(Models.Entities.Print.Template item)
    {
        Models.Entities.Print.Template template = templateRepo.Update(item);

        string zplKey = $"TEMPLATES:{template.Uid}";
        if (provider.KeyExists(zplKey))
            provider.StringSet(zplKey, template.Body, TimeSpan.FromHours(1));

        return template;
    }

    [Transactional]
    public void Delete(Models.Entities.Print.Template item) => templateRepo.Delete(item);

    [Transactional]
    public IEnumerable<Models.Entities.Print.Template> GetTemplatesByIsWeight(bool isWeight) =>
        templateRepo.GetTemplatesByIsWeight(isWeight);

    public string? GetTemplateByUidFromCacheOrDb(Guid templateUid)
    {
        string zplKey = $"TEMPLATES:{templateUid}";

        if (provider.KeyExists(zplKey))
            return provider.StringGet(zplKey);

        Models.Entities.Print.Template temp = GetItemByUid(templateUid);

        if (!temp.IsExists || temp.Body == string.Empty) return null;

        provider.StringSet(zplKey, temp.Body, TimeSpan.FromHours(1));
        return temp.Body;
    }
}