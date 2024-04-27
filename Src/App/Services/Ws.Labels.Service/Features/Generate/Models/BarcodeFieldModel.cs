using Ws.Labels.Service.Features.Generate.Common;

namespace Ws.Labels.Service.Features.Generate.Models;

public class BarcodeFieldModel<T>(string name, short length, bool isRepeatable=false) : IBarcodeFieldModel {
    public short Length { get; } = length;
    public bool IsRepeatable { get; } = isRepeatable;

    public Type Type => typeof(T);
    public string Name { get; } = name;


    public override bool Equals(object? obj)
    {
        if (obj is BarcodeFieldModel<T> other)
            return Name == other.Name;
        return false;
    }

    public override int GetHashCode() => Name.GetHashCode();
}