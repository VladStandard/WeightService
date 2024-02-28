using Ws.Database.Core.Entities.Ref.Templates;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Aspects;
using Ws.Domain.Services.Features.Template.Validators;

namespace Ws.Domain.Services.Features.Template;

internal class TemplateService(SqlTemplateRepository templateRepo) : ITemplateService
{
    [Transactional] 
    public IEnumerable<TemplateEntity> GetAll() => templateRepo.GetAll();
    
    [Transactional] 
    public TemplateEntity GetItemByUid(Guid uid) => templateRepo.GetByUid(uid);
    
    [Transactional, Validate<TemplateNewValidator>] 
    public TemplateEntity Create(TemplateEntity item) => templateRepo.Save(item);
    
    [Transactional, Validate<TemplateUpdateValidator>]
    public TemplateEntity Update(TemplateEntity item) => templateRepo.Update(item);
}