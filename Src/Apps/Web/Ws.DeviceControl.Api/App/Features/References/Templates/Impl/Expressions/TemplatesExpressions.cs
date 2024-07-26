using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Expressions;

internal static class TemplatesExpressions
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
            Body = template.Body,
            CreateDt = template.CreateDt,
            ChangeDt =  template.ChangeDt
        };
}