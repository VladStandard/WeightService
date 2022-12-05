// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using WebApiCore.Utils;


namespace WebApiCore.Models.WebResponses;

[XmlRoot(WebConstants.Response, Namespace = "", IsNullable = false)]
public class ResponseBarCodeListModel : SerializeBase, ISerializable //ICloneable, ISqlDbBase, 
{
    #region Public and private fields, properties, constructor

    [XmlArray(WebConstants.Barcodes), XmlArrayItem(WebConstants.Barcode)]
    public List<ResponseBarCodeModel> ResponseBarCodes { get; set; }

    public ResponseBarCodeListModel()
    {
        ResponseBarCodes = new();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private ResponseBarCodeListModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        object? barCodes = info.GetValue(nameof(ResponseBarCodes), typeof(List<ResponseBarCodeModel>));
        ResponseBarCodes = barCodes is not null ? (List<ResponseBarCodeModel>)barCodes : new();
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
        info.AddValue(nameof(ResponseBarCodes), ResponseBarCodes, typeof(ResponseBarCodeModel));
    }

    #endregion
}