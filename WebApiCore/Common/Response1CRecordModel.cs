// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System.Xml.Serialization;

namespace WebApiCore.Common;

[XmlRoot(Namespace = "", IsNullable = true)]
public class Response1CRecordModel : SerializeDeprecatedModel<Response1CRecordModel>
{
    #region Public and private fields and properties

    [XmlElement("Guid", IsNullable = true)]
    public Guid Uid { get; set; }
    [XmlElement("Message", IsNullable = false)]
    public string Message { get; set; }

    public Response1CRecordModel(Guid uid, string message)
    {
        Uid = uid;
        Message = message;
    }

    public Response1CRecordModel()
    {
        Uid = Guid.Empty;
        Message = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(Uid)}: {Uid}. " +
            $"{nameof(Message)}: {Message}. ";
    }

    #endregion
}
