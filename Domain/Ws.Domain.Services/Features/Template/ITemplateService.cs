using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Common;

namespace Ws.Domain.Services.Features.Template;

public interface ITemplateService : IAll<TemplateEntity>
{
    TemplateEntity GetById(long id);
}