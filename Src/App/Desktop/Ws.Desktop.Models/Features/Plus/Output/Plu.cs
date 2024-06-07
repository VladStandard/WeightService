namespace Ws.Desktop.Models.Features.Plus.Output;

public record Plu
{
    public Guid Uid;
    public ushort Number;
    public byte BundleCount;
    public string Name = string.Empty;
    public string FullName = string.Empty;

    public Package Clip = new();
    public Package Bundle = new();
    public Package Box = new();
};