using Ws.Database.EntityFramework.Entities.Zpl.Templates;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Create;
using Ws.DeviceControl.Models.Features.References.Template.Commands.Update;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;

internal static class TemplateDtoExtensions
{
    public static TemplateEntity ToEntity(this TemplateCreateDto dto)
    {
        return new()
        {
            Name = dto.Name,
            Body = dto.Body,
            IsWeight = dto.IsWeight,
            Width = dto.Width,
            Height = dto.Height,
            Rotate = dto.Rotate,
        };
    }

    public static void UpdateEntity(this TemplateUpdateDto dto, TemplateEntity entity)
    {
        entity.Name = dto.Name;
        entity.Rotate = dto.Rotate;
        entity.Width = dto.Width;
        entity.Height = dto.Height;
        entity.Body = dto.Body;
    }
}