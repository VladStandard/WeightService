using Ws.Domain.Models.Entities.Scale;
using Ws.Services.Common;

namespace Ws.Services.Features.Template;

public interface ITemplateService : IAll<TemplateEntity>
{
    TemplateEntity GetById(long id);
}