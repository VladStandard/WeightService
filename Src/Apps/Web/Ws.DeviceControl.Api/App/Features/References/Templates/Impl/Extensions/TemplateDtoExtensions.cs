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
            Width = (short)dto.Width,
            Height = (short)dto.Height,
            Rotate = (short)dto.Rotate,
        };
    }

    public static void UpdateEntity(this TemplateUpdateDto dto, TemplateEntity entity)
    {
        entity.Name = dto.Name;
        entity.Rotate = (short)dto.Rotate;
        entity.Width = (short)dto.Width;
        entity.Height = (short)dto.Height;
        entity.Body = dto.Body;
    }
}