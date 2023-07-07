// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsDataCore.Serialization;

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Record, Namespace = "", IsNullable = false)]
public sealed class WsResponse1CErrorModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    [XmlAttribute(nameof(Message))]
    public string Message { get; set; }

    public WsResponse1CErrorModel()
    {
        Uid = Guid.Empty;
        Message = string.Empty;
    }

    public WsResponse1CErrorModel(Guid uid, string message)
    {
        Uid = uid;
        Message = message;
    }

    public WsResponse1CErrorModel(Exception ex)
    {
        Uid = Guid.Empty;
        Message = ex.Message;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private WsResponse1CErrorModel(SerializationInfo info, StreamingContext context) : base(info, context)
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