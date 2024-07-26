using Ws.Database.EntityFramework.Entities.Zpl.Templates;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;

internal static class TemplatesExpressions
{
    public static Expression<Func<TemplateEntity, ProxyDto>> ToProxy =>
        template => new()
        {
            Id = template.Id,
            Name = template.Name
        };
}