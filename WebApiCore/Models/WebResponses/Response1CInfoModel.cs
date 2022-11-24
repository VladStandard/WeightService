// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.Serialization;
using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.Info, Namespace = "", IsNullable = false)]
public class Response1CInfoModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlAttribute(nameof(Message))]
    public string Message { get; set; }

    public Response1CInfoModel(string message)
    {
        Message = message;
    }

    public Response1CInfoModel()
    {
        Message = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1CInfoModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Message = info.GetString(nameof(Message)) as string ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => 
        $"{nameof(Message)}: {Message}. ";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Message), Message);
    }

    #endregion
}
