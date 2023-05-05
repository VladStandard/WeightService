// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

[Serializable]
public class WsSqlSerializeBase : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public WsSqlConnectFactory SqlConnect { get; private set; } = WsSqlConnectFactory.Instance;

	/// <summary>
	/// Constructor.
	/// </summary>
	public WsSqlSerializeBase()
    {
	    //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsSqlSerializeBase(SerializationInfo info, StreamingContext context)
    {
        //SqlConnect = (SqlConnectFactory)info.GetValue(nameof(SqlConnect), typeof(SqlConnectFactory));
    }

    #endregion
}
