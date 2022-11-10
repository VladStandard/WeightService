// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System.Xml.Serialization;
using WebApiCore.Utils;

namespace WebApiCore.Common;

[XmlRoot("Response", Namespace = "", IsNullable = false)]
public class Response1CModel : SerializeDeprecatedModel<Response1CModel>
{
    #region Public and private fields and properties

    [XmlAttribute(nameof(SuccessesCount))]
    public int SuccessesCount { get; set; }
    [XmlAttribute(nameof(ErrorsCount))]
    public int ErrorsCount { get; set; }
    [XmlElement("SqlQuery")]
    public ResponseQueryModel ResponseQuery { get; set; }
    [XmlArray(nameof(Successes)), XmlArrayItem("Record")]
    public List<Response1CRecordModel> Successes { get; set; }
    [XmlArray(nameof(Errors)), XmlArrayItem("Record")]
    public List<Response1CRecordModel> Errors { get; set; }

    public Response1CModel(List<Response1CRecordModel> successes, List<Response1CRecordModel> errors, 
        ResponseQueryModel query)
    {
        Successes = successes;
        Errors = errors;
        ResponseQuery = query;
    }

    public Response1CModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        Successes = new();
        Errors = new();
        ResponseQuery = new();
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(SuccessesCount)}: {SuccessesCount}. " +
            $"{nameof(ErrorsCount)}: {ErrorsCount}. ";
    }

    #endregion
}
