using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Shared;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;

internal static class TemplateExpressions
{
    public static Expression<Func<TemplateEntity, ProxyDto>> ToProxy =>
        template => new()
        {
            Id = template.Id,
            Name = template.Name
        };

    public static Expression<Func<TemplateEntity, TemplateDto>> ToDto =>
        template => new()
        {
            Id = template.Id,
            Name = template.Name,
            IsWeight = template.IsWeight,
            Width = (ushort)template.Width,
            Height = (ushort)template.Height,
            Rotate = (ushort)template.Rotate,
            CreateDt = template.CreateDt,
            ChangeDt =  template.ChangeDt
        };
}