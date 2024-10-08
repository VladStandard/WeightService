using Ws.DeviceControl.Models.Features.References1C.Plus.Commands.Update;
using Ws.DeviceControl.Models.Features.References1C.Plus.Queries;

namespace Ws.DeviceControl.Models.Features.References1C.Plus;

public static class PluMapper
{
    public static PluUpdateDto DtoToUpdateDto(PluDto item)
    {
        return new()
        {
            TemplateId = item.Template?.Id ?? Guid.Empty,
        };
    }
}