// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

public interface IWsSqlObjectBase
{
    string ToString();
    bool Equals(object obj);
    bool EqualsNew();
    int GetHashCode();
    void GetObjectData(SerializationInfo info, StreamingContext context);
    void FillProperties();
}