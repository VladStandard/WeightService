// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Common;

[Serializable]
public class WsSqlSerializeBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public WsSqlConnectFactory SqlConnect { get; private set; } = WsSqlConnectFactory.Instance;

    public WsSqlSerializeBase() { }
    
    protected WsSqlSerializeBase(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        //
    }

    #endregion
}
