// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Serialization.Models;

namespace WsWebApi.Models.WebResponses;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class ResponseDebugInfoModel : SerializeDebugBase
{
    #region Public and private fields and properties

    [XmlElement("DebugInfo")]
    public ServiceInfoModel? Info { get; set; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public ResponseDebugInfoModel()
    {
        //
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public ResponseDebugInfoModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Info = (ServiceInfoModel?)info.GetValue(nameof(Info), typeof(ServiceInfoModel));
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