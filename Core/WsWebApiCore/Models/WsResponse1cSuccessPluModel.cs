// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsWebApiCore.Models;

[XmlRoot(WsWebConstants.Record, Namespace = "", IsNullable = false)]
public class WsResponse1CSuccessPluModel : SerializeBase
{
    #region Public and private fields, properties, constructor

    [XmlAttribute("Guid")]
    public Guid Uid { get; set; }

    [XmlAttribute("Description")]
    public string Description { get; set; }

    public WsResponse1CSuccessPluModel()
    {
        Uid = Guid.Empty;
        Description = string.Empty;
    }

    public WsResponse1CSuccessPluModel(Guid uid, string description)
    {
        Uid = uid;
        Description = description;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private WsResponse1CSuccessPluModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? uid = info.GetValue(nameof(Uid), typeof(Guid));
        Uid = uid is not null ? (Guid)uid : Guid.Empty;
        Description = info.GetString(nameof(Description)) ?? string.Empty;
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
        info.AddValue(nameof(Description), Description);
    }

    #endregion
}