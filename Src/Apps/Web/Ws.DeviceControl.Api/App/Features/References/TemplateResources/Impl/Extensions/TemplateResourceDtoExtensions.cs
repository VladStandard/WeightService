using Ws.Database.Entities.Zpl.ZplResources;
using Ws.DeviceControl.Models.Features.References.TemplateResources.Commands;

namespace Ws.DeviceControl.Api.App.Features.References.TemplateResources.Impl.Extensions;

internal static class TemplateResourceDtoExtensions
{
    public static ZplResourceEntity ToEntity(this TemplateResourceCreateDto dto)
    {
        return new()
        {
            Name = dto.Name,
            Zpl = dto.Body,
            Type = dto.Type
        };
    }

    public static void UpdateEntity(this TemplateResourceUpdateDto dto, ZplResourceEntity entity)
    {
        entity.Name = dto.Name;
        entity.Zpl = dto.Body;
        entity.Type = dto.Type;
    }
}