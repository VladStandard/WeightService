using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Models.Features.References.TemplateResources;

public static class TemplateResourceMapper
{
    public static TemplateResourceUpdateDto DtoToUpdateDto(TemplateResourceDto item)
    {
        return new()
        {
            Name = item.Name,
            Body = string.Empty,
            Type = item.Type
        };
    }
}