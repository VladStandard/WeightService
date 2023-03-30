// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Serialization.Models;

namespace WsWebApi.Models;

[Serializable]
[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class WsResponseBarCodeListModel : SerializeBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlArray(WebConstants.Barcodes), XmlArrayItem(WebConstants.Barcode)]
    public List<WsResponseBarCodeModel> ResponseBarCodes { get; set; }

    [XmlAttribute(nameof(StartDate))] public DateTime StartDate { get; set; }

    [XmlAttribute(nameof(EndDate))] public DateTime EndDate { get; set; }

    [XmlAttribute(nameof(Count))]
    public int Count { get; set; }


    public WsResponseBarCodeListModel()
    {
        ResponseBarCodes = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected WsResponseBarCodeListModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? barCodes = info.GetValue(nameof(ResponseBarCodes), typeof(List<WsResponseBarCodeModel>));
        ResponseBarCodes = barCodes is not null ? (List<WsResponseBarCodeModel>)barCodes : new();
        StartDate = info.GetDateTime(nameof(StartDate));
        EndDate = info.GetDateTime(nameof(EndDate));
        Count = info.GetInt32(nameof(Count));
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
        info.AddValue(nameof(ResponseBarCodes), ResponseBarCodes, typeof(WsResponseBarCodeModel));
        info.AddValue(nameof(StartDate), StartDate);
        info.AddValue(nameof(EndDate), EndDate);
        info.AddValue(nameof(Count), Count);
    }

    #endregion
}