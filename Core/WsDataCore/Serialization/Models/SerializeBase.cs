// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCore.Serialization.Models;

/// <summary>
/// Базвый класс сериализуемого объекта.
/// </summary>
[Serializable]
public class SerializeBase : ISerializable
{
    #region Public and private methods - ISerializable

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public SerializeBase()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SerializeBase(SerializationInfo info, StreamingContext context)
    {
        //
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        //
    }

    #endregion
}