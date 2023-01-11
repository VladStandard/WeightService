// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace WsWebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.Record, Namespace = "", IsNullable = false)]
public class Response1cRecordModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    [XmlAttribute(nameof(Message))]
    public string Message { get; set; }

    [XmlAttribute(nameof(InnerMessage))]
    public string? InnerMessage { get; set; }

    public Response1cRecordModel()
    {
        Uid = Guid.Empty;
        Message = string.Empty;
        InnerMessage = null;
    }

    public Response1cRecordModel(Guid uid, string message, string innerMessage)
    {
        Uid = uid;
        Message = message;
        InnerMessage = innerMessage;
    }

    public Response1cRecordModel(Guid uid, string message) : this(uid, message, string.Empty) { }

    public Response1cRecordModel(Exception ex)
    {
        Uid = Guid.Empty;
        Message = ex.Message;
        InnerMessage = ex.InnerException is not null ? ex.InnerException.Message : null;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1cRecordModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? uid = info.GetValue(nameof(Uid), typeof(Guid));
        Uid = uid is not null ? (Guid)uid : Guid.Empty;
        Message = info.GetString(nameof(Message)) as string ?? string.Empty;
        InnerMessage = info.GetString(nameof(InnerMessage)) as string ?? null;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(Uid)}: {Uid}. " +
        $"{nameof(Message)}: {Message}. " +
        $"{nameof(InnerMessage)}: {InnerMessage}. ";

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
        info.AddValue(nameof(InnerMessage), InnerMessage);
    }

    #endregion
}