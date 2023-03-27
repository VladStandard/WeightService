// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApi.Models.WebResponses;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class Response1cShortModel : ResponseDebugInfoModel
{
    #region Public and private fields and properties

    [XmlAttribute]
    public int SuccessesCount { get => Successes.Count; set => _ = value; }
    [XmlAttribute]
    public int ErrorsCount { get => Errors.Count; set => _ = value; }

    [XmlElement("SqlQuery")]
    public ResponseQueryModel? ResponseQuery { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<Response1cSuccessModel> Successes { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<Response1cErrorModel> Errors { get; set; }
    
    public Response1cShortModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        ResponseQuery = null;
        Successes = new();
        Errors = new();
    }

    public Response1cShortModel(List<Response1cSuccessModel> successes,
        List<Response1cErrorModel> errors, ResponseQueryModel? responseQuery) : this()
    {
        Successes = successes;
        Errors = errors;
        ResponseQuery = responseQuery;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1cShortModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SuccessesCount = info.GetInt32(nameof(SuccessesCount));
        ErrorsCount = info.GetInt32(nameof(ErrorsCount));
        ResponseQuery = info.GetValue(nameof(ResponseQuery), typeof(ResponseQueryModel)) as ResponseQueryModel ?? null;
        Successes = info.GetValue(nameof(Successes), typeof(List<Response1cSuccessModel>)) as List<Response1cSuccessModel> ?? new();
        Errors = info.GetValue(nameof(Errors), typeof(List<Response1cErrorModel>)) as List<Response1cErrorModel> ?? new();
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(SuccessesCount)}: {SuccessesCount}. " +
        $"{nameof(ErrorsCount)}: {ErrorsCount}. " +
        $"{nameof(Successes)}.{nameof(Successes.Count)}: {Successes.Count}. " +
        $"{nameof(Errors)}.{nameof(Errors.Count)}: {Errors.Count}. ";

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