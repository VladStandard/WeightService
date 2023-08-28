namespace WsDataCore.Serialization;

[Serializable]
public class SerializeDebugBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public bool IsDebug { get; set; }

    public SerializeDebugBase()
    {
        IsDebug = false;
    }

    public SerializeDebugBase(bool isDebug)
    {
        IsDebug = isDebug;
    }

    protected SerializeDebugBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        IsDebug = info.GetBoolean(nameof(IsDebug));
    }

    public SerializeDebugBase(SerializeDebugBase item) : base(item)
    {
        IsDebug = item.IsDebug;
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