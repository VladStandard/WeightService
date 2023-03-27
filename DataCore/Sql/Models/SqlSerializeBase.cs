// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;
using DataCore.Sql.Core.Helpers;

namespace DataCore.Sql.Models;

[Serializable]
public class SqlSerializeBase : SerializeBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlIgnore] public SqlConnectFactory SqlConnect { get; private set; } = SqlConnectFactory.Instance;

	/// <summary>
	/// Constructor.
	/// </summary>
	public SqlSerializeBase()
    {
	    //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected SqlSerializeBase(SerializationInfo info, StreamingContext context)
    {
        //SqlConnect = (SqlConnectFactory)info.GetValue(nameof(SqlConnect), typeof(SqlConnectFactory));
    }

    #endregion
}
