// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Response, Namespace = "", IsNullable = false)]
public class WsResponse1CShortModel : WsResponseDebugInfoModel
{
    #region Public and private fields, properties, constructor

    [XmlAttribute]
    public int SuccessesCount { get => Successes.Count; set => _ = value; }
    [XmlAttribute]
    public int ErrorsCount { get => Errors.Count; set => _ = value; }

    [XmlElement("SqlQuery")]
    public WsResponseQueryModel? ResponseQuery { get; set; }

    [XmlArray(WsWebConstants.Successes), XmlArrayItem(WsWebConstants.Record)]
    public List<WsResponse1CSuccessModel> Successes { get; set; }

    [XmlArray(WsWebConstants.Errors), XmlArrayItem(WsWebConstants.Record)]
    public List<WsResponse1CErrorModel> Errors { get; set; }

    public WsResponse1CShortModel()
    {
        SuccessesCount = 0;
        ErrorsCount = 0;
        ResponseQuery = null;
        Successes = new();
        Errors = new();
    }

    public WsResponse1CShortModel(List<WsResponse1CSuccessModel> successes, List<WsResponse1CSuccessPluModel> successesPlus,
        List<WsResponse1CErrorModel> errors, WsResponseQueryModel? responseQuery) : this()
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
    private WsResponse1CShortModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        SuccessesCount = info.GetInt32(nameof(SuccessesCount));
        ErrorsCount = info.GetInt32(nameof(ErrorsCount));
        ResponseQuery = info.GetValue(nameof(ResponseQuery), typeof(WsResponseQueryModel)) as WsResponseQueryModel ?? null;
        Successes = info.GetValue(nameof(Successes), typeof(List<WsResponse1CSuccessModel>)) as List<WsResponse1CSuccessModel> ?? new();
        Errors = info.GetValue(nameof(Errors), typeof(List<WsResponse1CErrorModel>)) as List<WsResponse1CErrorModel> ?? new();
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