namespace WsStorageCore.Common;

[Serializable]
public class WsSqlSerializeBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public WsSqlConnectFactory SqlConnect { get; private set; } = WsSqlConnectFactory.Instance;

    public WsSqlSerializeBase() {}

    protected WsSqlSerializeBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
    }

    #endregion
}