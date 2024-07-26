using Ws.DeviceControl.Models.Dto.References.Template.Commands.Update;
using Ws.DeviceControl.Models.Dto.References.Template.Queries;

namespace Ws.DeviceControl.Models.Dto.References.Template;

public static class TemplateMapper
{
    public static TemplateUpdateDto DtoToUpdateDto(TemplateDto item)
    {
        return new()
        {
            Name = item.Name,
            Width = item.Width,
            Height = item.Height,
            Rotate = item.Rotate,
            Body = item.Body
        };
    }
}