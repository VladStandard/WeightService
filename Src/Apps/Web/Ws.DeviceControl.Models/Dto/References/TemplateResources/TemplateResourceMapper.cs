using Ws.DeviceControl.Models.Dto.References.TemplateResources.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.TemplateResources.Queries;

namespace Ws.DeviceControl.Models.Dto.References.TemplateResources;

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