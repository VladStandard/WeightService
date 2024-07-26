using Ws.Database.EntityFramework.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Expressions;

internal static class TemplateResourceExpressions
{
    public static Expression<Func<ZplResourceEntity, TemplateResourceDto>> ToDto =>
        templateResource => new()
        {
            Id = templateResource.Id,
            Name = templateResource.Name,
            Type = templateResource.Type,
            CreateDt = templateResource.CreateDt,
            ChangeDt =  templateResource.ChangeDt
        };
}