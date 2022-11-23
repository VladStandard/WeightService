// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Runtime.Serialization;
using System.Xml.Serialization;
using DataCore.Sql.Models;
using WebApiCore.Utils;

namespace WebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class Response1CModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlAttribute(nameof(SuccessesCount))]
    public int SuccessesCount { get; set; }
    [XmlAttribute(nameof(ErrorsCount))]
    public int ErrorsCount { get; set; }
    [XmlElement("SqlQuery")]
    public ResponseQueryModel? ResponseQuery { get; set; }
    [XmlArray(nameof(Successes)), XmlArrayItem("Record")]
    public List<Response1CRecordModel> Successes { get; set; }
    [XmlArray(nameof(Errors)), XmlArrayItem("Record")]
    public List<Response1CRecordModel> Errors { get; set; }

    public Response1CModel(List<Response1CRecordModel> successes, List<Response1CRecordModel> errors,
        ResponseQueryModel? responseQuery)
    {
        Successes = successes;
        Errors = errors;
        ResponseQuery = responseQuery;
    }

    public Response1CModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        ResponseQuery = null;
        Successes = new();
        Errors = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1CModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SuccessesCount = info.GetInt32(nameof(SuccessesCount));
        ErrorsCount = info.GetInt32(nameof(ErrorsCount));
        ResponseQuery = info.GetValue(nameof(ResponseQuery), typeof(ResponseQueryModel)) as ResponseQueryModel ?? null;
        Successes = info.GetValue(nameof(Successes), typeof(List<Response1CRecordModel>)) as List<Response1CRecordModel> ?? new();
        Errors = info.GetValue(nameof(Errors), typeof(List<Response1CRecordModel>)) as List<Response1CRecordModel> ?? new();
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
            $"{nameof(SuccessesCount)}: {SuccessesCount}. " +
            $"{nameof(ErrorsCount)}: {ErrorsCount}. ";
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(SuccessesCount), SuccessesCount);
        info.AddValue(nameof(ErrorsCount), ErrorsCount);
        info.AddValue(nameof(ResponseQuery), ResponseQuery);
        info.AddValue(nameof(Successes), Successes);
        info.AddValue(nameof(Errors), Errors);
    }

    #endregion
}
