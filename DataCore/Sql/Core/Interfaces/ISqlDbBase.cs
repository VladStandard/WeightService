// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Core.Interfaces;

public interface ISqlDbBase
{
    string ToString();
    bool Equals(object obj);
    bool EqualsNew();
    int GetHashCode();
    object Clone();
    void GetObjectData(SerializationInfo info, StreamingContext context);
    void ClearNullProperties();
    void FillProperties();
}