// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Info, Namespace = "", IsNullable = false)]
public sealed class WsServiceReplyModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    public WsServiceReplyModel(string message)
    {
        Message = message;
    }

    public WsServiceReplyModel()
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            @$"{nameof(Message)}: {Message}. ";
    }

    #endregion
}