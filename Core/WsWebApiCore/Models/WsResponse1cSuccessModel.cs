namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Record, Namespace = "", IsNullable = false)]
public sealed class WsResponse1CSuccessModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    public WsResponse1CSuccessModel()
    {
        Uid = Guid.Empty;
    }

    public WsResponse1CSuccessModel(Guid uid)
    {
        Uid = uid;
    }

    private WsResponse1CSuccessModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? uid = info.GetValue(nameof(Uid), typeof(Guid));
        Uid = uid is not null ? (Guid)uid : Guid.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() => $"{nameof(Uid)}: {Uid}. ";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Uid), Uid);
    }

    #endregion
}