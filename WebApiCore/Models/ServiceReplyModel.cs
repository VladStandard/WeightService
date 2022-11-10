// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Models;

[XmlRoot(WebConstants.Info, Namespace = "", IsNullable = false)]
public class ServiceReplyModel : SerializeBase
{
    #region Public and private fields and properties

    /// <summary>
    /// Message.
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="message"></param>
    public ServiceReplyModel(string message)
    {
        Message = message;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    public ServiceReplyModel()
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
