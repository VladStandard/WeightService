// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsStorageCore.Common;

/// <summary>
/// Интерфейс базового объекта SQL.
/// </summary>
public interface IWsSqlObjectBase
{
    string ToString();
    bool Equals(object obj);
    bool EqualsNew();
    int GetHashCode();
    void GetObjectData(SerializationInfo info, StreamingContext context);
    void ClearNullProperties();
    void FillProperties();
}