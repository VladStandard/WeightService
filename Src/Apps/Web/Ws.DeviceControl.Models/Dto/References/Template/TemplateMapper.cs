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
            Width = (short) item.Width,
            Height = (short) item.Height,
            Rotate = (short) item.Rotate,
        };
    }
}