namespace Ws.Labels.Service.Features.Generate.Common;

public interface IBarcodeFieldModel
{
    Type Type { get; }
    string Name { get; }
    short Length { get; }
    bool IsRepeatable { get; }
}