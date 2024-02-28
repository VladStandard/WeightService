using Ws.Database.Core.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Template;

public class TemplateService(SqlTemplateRepository templateRepo) : ITemplateService
{
    [Transactional] public IEnumerable<TemplateEntity> GetAll() => templateRepo.GetAll();
    [Transactional] public TemplateEntity GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);
    [Transactional] public TemplateEntity Create(TemplateEntity item) => templateRepo.Save(item);
    [Transactional] public TemplateEntity Update(TemplateEntity item) => templateRepo.Update(item);
}