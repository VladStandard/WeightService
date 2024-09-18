using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public sealed record ExtendedBarcodeItemDto: BarcodeItemDto
{
    public ushort Length { get; set; }

    public bool IsConst { get; set; }
}