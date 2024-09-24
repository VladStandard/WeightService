using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public sealed record ExtendedBarcodeItemDto: BarcodeItemDto
{
    public Guid Id { get; } = Guid.NewGuid();
    public ushort Length { get; set; } = 0;

    public bool IsConst { get; set; } = true;
}