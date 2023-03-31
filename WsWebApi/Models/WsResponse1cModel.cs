// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable InconsistentNaming

namespace WsWebApi.Models;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public sealed class WsResponse1cModel : WsResponseDebugInfoModel
{
    #region Public and private fields and properties

    [XmlAttribute]
    public int SuccessesCount { get; set; }

    [XmlAttribute]
    public int ErrorsCount { get; set; }

    [XmlElement("SqlQuery")]
    public WsResponseQueryModel? ResponseQuery { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Info)]
    public List<WsResponse1cInfoModel> Infos { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<WsResponse1cRecordModel> Successes { get; set; }

    [XmlArray, XmlArrayItem(WebConstants.Record)]
    public List<WsResponse1cRecordModel> Errors { get; set; }

    /// <summary>
    /// Empty constructor.
    /// </summary>
    public WsResponse1cModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        ResponseQuery = null;
        Infos = new();
        Successes = new();
        Errors = new();
    }

    public WsResponse1cModel(List<WsResponse1cInfoModel> infos, List<WsResponse1cRecordModel> successes,
        List<WsResponse1cRecordModel> errors, WsResponseQueryModel? responseQuery) : this()
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
    private WsResponse1cModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SuccessesCount = info.GetInt32(nameof(SuccessesCount));
        ErrorsCount = info.GetInt32(nameof(ErrorsCount));
        ResponseQuery = info.GetValue(nameof(ResponseQuery), typeof(WsResponseQueryModel)) as WsResponseQueryModel ?? null;
        Infos = info.GetValue(nameof(Infos), typeof(List<WsResponse1cInfoModel>)) as List<WsResponse1cInfoModel> ?? new();
        Successes = info.GetValue(nameof(Successes), typeof(List<WsResponse1cRecordModel>)) as List<WsResponse1cRecordModel> ?? new();
        Errors = info.GetValue(nameof(Errors), typeof(List<WsResponse1cRecordModel>)) as List<WsResponse1cRecordModel> ?? new();
    }

    #endregion

    #region Public and private methods

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

    public override string ToString() =>
        $"{nameof(SuccessesCount)}: {SuccessesCount}. " +
        $"{nameof(ErrorsCount)}: {ErrorsCount}. " +
        $"{nameof(Infos)}.{nameof(Infos.Count)}: {Infos.Count}. " +
        $"{nameof(Successes)}.{nameof(Successes.Count)}: {Successes.Count}. " +
        $"{nameof(Errors)}.{nameof(Errors.Count)}: {Errors.Count}. ";

    #endregion
}