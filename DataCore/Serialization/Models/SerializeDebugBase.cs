// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Serialization.Models;

[Serializable]
public class SerializeDebugBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public bool IsDebug { get; set; }

    #endregion

    #region Public and private methods - ISerializable

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public SerializeDebugBase()
    {
        IsDebug = false;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public SerializeDebugBase(bool isDebug)
    {
        IsDebug = isDebug;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SerializeDebugBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsDebug = info.GetBoolean(nameof(IsDebug));
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(IsDebug), IsDebug);
    }

    #endregion
}