using Ws.Database.Core.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;

namespace Ws.Domain.Services.Features.Template;

public class TemplateService(SqlTemplateRepository templateRepo) : ITemplateService
{
    [Session] public IEnumerable<TemplateEntity> GetAll() => templateRepo.GetAll();
    [Session] public TemplateEntity GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);
}