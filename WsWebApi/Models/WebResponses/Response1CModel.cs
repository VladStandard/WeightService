// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApi.Models.WebResponses;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class Response1cModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlAttribute]
    public int SuccessesCount { get; set; }

    [XmlAttribute]
    public int ErrorsCount { get; set; }

    [XmlElement("SqlQuery")]
    public ResponseQueryModel? ResponseQuery { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Info)]
    public List<Response1cInfoModel> Infos { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<Response1cRecordModel> Successes { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<Response1cRecordModel> Errors { get; set; }

    public Response1cModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        ResponseQuery = null;
        Infos = new();
        Successes = new();
        Errors = new();
    }

    public Response1cModel(List<Response1cInfoModel> infos, List<Response1cRecordModel> successes,
        List<Response1cRecordModel> errors, ResponseQueryModel? responseQuery) : this()
    {
        Infos = infos;
        Successes = successes;
        Errors = errors;
        ResponseQuery = responseQuery;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private Response1cModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SuccessesCount = info.GetInt32(nameof(SuccessesCount));
        ErrorsCount = info.GetInt32(nameof(ErrorsCount));
        ResponseQuery = info.GetValue(nameof(ResponseQuery), typeof(ResponseQueryModel)) as ResponseQueryModel ?? null;
        Infos = info.GetValue(nameof(Infos), typeof(List<Response1cInfoModel>)) as List<Response1cInfoModel> ?? new();
        Successes = info.GetValue(nameof(Successes), typeof(List<Response1cRecordModel>)) as List<Response1cRecordModel> ?? new();
        Errors = info.GetValue(nameof(Errors), typeof(List<Response1cRecordModel>)) as List<Response1cRecordModel> ?? new();
        //Count = info.GetInt32(nameof(Count));
        //object? brands = info.GetValue(nameof(Brands), typeof(List<BrandModel>));
        //Brands = brands is not null ? (List<BrandModel>)brands : new();
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(SuccessesCount)}: {SuccessesCount}. " +
        $"{nameof(ErrorsCount)}: {ErrorsCount}. " +
        $"{nameof(Infos)}.{nameof(Infos.Count)}: {Infos.Count}. " +
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
        info.AddValue(nameof(Infos), Infos);
        info.AddValue(nameof(Successes), Successes);
        info.AddValue(nameof(Errors), Errors);
    }

    #endregion
}