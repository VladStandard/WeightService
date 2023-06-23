// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Info, Namespace = "", IsNullable = false)]
public class WsResponse1CInfoModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute(nameof(Message))]
    public string Message { get; set; }

    public WsResponse1CInfoModel(string message)
    {
        Message = message;
    }

    public WsResponse1CInfoModel()
    {
        Message = string.Empty;
    }

    private WsResponse1CInfoModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Message = info.GetString(nameof(Message)) ?? string.Empty;
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