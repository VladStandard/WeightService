// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public class WsResponseDebugInfoModel : SerializeDebugBase
{
    #region Public and private fields, properties, constructor

    [XmlElement(WsWebConstants.DebugInfo)]
    public WsServiceInfoModel? Info { get; set; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsResponseDebugInfoModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public WsResponseDebugInfoModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Info = (WsServiceInfoModel?)info.GetValue(nameof(Info), typeof(WsServiceInfoModel));
    }

    #endregion

    #region Public and private methods

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Info), Info);
    }

    #endregion
}