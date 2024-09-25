using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace DeviceControl.Source.Features.BarcodeConfigurator;

public sealed record ExtendedBarcodeItemDto: BarcodeItemDto
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Example { get; set; } = string.Empty;
    public bool IsConst { get; set; } = true;
    public int DefaultLength { get; set; } = -1;
    public string CachedMask { get; set; } = string.Empty;
}