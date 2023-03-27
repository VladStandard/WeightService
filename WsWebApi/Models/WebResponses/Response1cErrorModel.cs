// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

using DataCore.Serialization.Models;

namespace WsWebApi.Models.WebResponses;

[XmlRoot(WebConstants.Record, Namespace = "", IsNullable = false)]
public class Response1cErrorModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    [XmlAttribute(nameof(Message))]
    public string Message { get; set; }

    public Response1cErrorModel()
    {
        Uid = Guid.Empty;
        Message = string.Empty;
    }

    public Response1cErrorModel(Guid uid, string message)
    {
        Uid = uid;
        Message = message;
    }

    public Response1cErrorModel(Exception ex)
    {
        Uid = Guid.Empty;
        Message = ex.Message;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1cErrorModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? uid = info.GetValue(nameof(Uid), typeof(Guid));
        Uid = uid is not null ? (Guid)uid : Guid.Empty;
        Message = info.GetString(nameof(Message)) ?? string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{nameof(Uid)}: {Uid}. {nameof(Message)}: {Message}";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Uid), Uid);
        info.AddValue(nameof(Message), Message);
    }

    #endregion
}