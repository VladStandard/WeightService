// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;
using DataCore.Sql.TableScaleModels;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using WebApiCore.Utils;

namespace WebApiCore.Models;

[XmlRoot(ElementName = WebConstants.Brands, Namespace = "", IsNullable = true)]
public class BrandListModel : SerializeBase
{
    #region Public and private fields and properties

    [XmlArray("Brands"), XmlArrayItem("Brand")]
    public List<BrandModel> Brands { get; set; } = new();

    /// <summary>
    /// Constructor.
    /// </summary>
    public BrandListModel()
    {
        //
    }

    ///// <summary>
    ///// Constructor.
    ///// </summary>
    //public BrandListModel(List<BrandModel> brands)
    //{
    //    Brands = brands;
    //}

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private BrandListModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Brands = (List<BrandModel>)info.GetValue(nameof(Brands), typeof(List<BrandModel>));
        //Brands = (BrandModel)info.GetValue(nameof(Brands), typeof(BrandModel));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        string result =
            @$"{nameof(Brands)}: {Brands.Count}. ";
        return result;
    }

    public string GetValue() => @$"{Brands}";

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Brands), Brands);
    }

    #endregion
}
