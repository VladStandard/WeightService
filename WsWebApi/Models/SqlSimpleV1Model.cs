// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Simple, Namespace = "", IsNullable = false)]
public class SqlSimpleV1Model : SerializeDebugBase
{
    #region Public and private fields and properties

    [XmlAttribute]
    public string Description { get; set; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public SqlSimpleV1Model()
    {
        Description = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="description"></param>
    /// <param name="isDebug"></param>
    public SqlSimpleV1Model(string description, bool isDebug) : base(isDebug)
    {
        Description = description;
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private SqlSimpleV1Model(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Description = info.GetString(nameof(Description)) ?? string.Empty;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Description), Description);
    }

    public override string ToString() =>
        @$"{nameof(Description)}: {Description}";

    #endregion
}
