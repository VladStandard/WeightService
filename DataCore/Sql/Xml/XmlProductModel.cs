// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Xml;

/// <summary>
/// XML-класс продукта.
/// </summary>
[Serializable]
public class XmlProductModel : ISerializable, ISqlDbBase
{
    #region Public and private fields, properties, constructor

    public string Category { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public string Comment { get; set; }
    public string Sku { get; set; }
    public string DescriptionOptional { get; set; }
    public Guid GuidMercury { get; set; }
    public string Temperature { get; set; }
    public string ProductShelfLife { get; set; }
    public short ProductShelfLifeShort
    {
        get
        {
            if (ProductShelfLife.EndsWith(" сут.", StringComparison.InvariantCultureIgnoreCase) ||
                ProductShelfLife.EndsWith(" сут,", StringComparison.InvariantCultureIgnoreCase))
            {
                //short.TryParse(ProductShelfLife[0..^5], out short value);
                short.TryParse(ProductShelfLife.AsSpan()[0..^5].ToString(), out short value);
                return value;
            }
            return 0;
        }
        set => ProductShelfLife = $"{value}  сут.";
    }
    public string Brand { get; set; }
    public List<XmlProductUnitModel> Units { get; set; }
    public List<XmlProductBarcodeModel> Barcodes { get; set; }
    public List<XmlProductBoxModel> Boxes { get; set; }
    public List<XmlProductBoxModel> Packs { get; set; }
    public string NameFull { get; set; }
    public string AdditionalDescriptionOfNomenclature { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public XmlProductModel()
    {
	    Category = string.Empty;
	    Code = string.Empty;
	    Description = string.Empty;
	    Comment = string.Empty;
	    Sku = string.Empty;
	    DescriptionOptional = string.Empty;
	    GuidMercury = Guid.Empty;
	    Temperature = string.Empty;
	    ProductShelfLife = string.Empty;
	    Brand = string.Empty;
	    Units = new();
	    Barcodes = new();
	    Boxes = new();
	    Packs = new();
	    NameFull = string.Empty;
	    AdditionalDescriptionOfNomenclature = string.Empty;
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private XmlProductModel(SerializationInfo info, StreamingContext context)
    {
	    Category = info.GetString(nameof(Category));
	    Code = info.GetString(nameof(Code));
	    Description = info.GetString(nameof(Description));
	    Comment = info.GetString(nameof(Comment));
	    Sku = info.GetString(nameof(Sku));
	    DescriptionOptional = info.GetString(nameof(DescriptionOptional));
	    GuidMercury = (Guid)info.GetValue(nameof(GuidMercury), typeof(Guid));
	    Temperature = info.GetString(nameof(Temperature));
	    ProductShelfLife = info.GetString(nameof(ProductShelfLife));
	    Brand = info.GetString(nameof(Brand));
	    Units = (List<XmlProductUnitModel>)info.GetValue(nameof(Units), typeof(List<XmlProductUnitModel>));
	    Barcodes = (List<XmlProductBarcodeModel>)info.GetValue(nameof(Barcodes), typeof(List<XmlProductBarcodeModel>));
	    Boxes = (List<XmlProductBoxModel>)info.GetValue(nameof(Boxes), typeof(List<XmlProductBoxModel>));
	    Packs = (List<XmlProductBoxModel>)info.GetValue(nameof(Packs), typeof(List<XmlProductBoxModel>));
	    NameFull = info.GetString(nameof(NameFull));
	    AdditionalDescriptionOfNomenclature = info.GetString(nameof(AdditionalDescriptionOfNomenclature));
	}

	#endregion

	#region Public and private methods

	public override string ToString()
    {
        string? strUnits = $"{Units.Count}. ";
        foreach (XmlProductUnitModel? unit in Units)
        {
	        strUnits += $"{unit}. ";
        }

        string strBarcodes = $"{Barcodes.Count}. ";
        foreach (XmlProductBarcodeModel? barcode in Barcodes)
        {
	        strBarcodes += $"{barcode}. ";
        }

        string strBoxes = $"{Boxes.Count}. ";
        foreach (XmlProductBoxModel? box in Boxes)
        {
	        strBoxes += $"{box}. ";
        }

        string strPacks = $"{Packs.Count}. ";
        foreach (XmlProductBoxModel? pack in Packs)
        {
	        strPacks += $"{pack}. ";
        }

        return
            $"{nameof(Category)}: {Category}. " +
            $"{nameof(Code)}: {Code}. " +
            $"{nameof(Description)}: {Description}. " +
            $"{nameof(Comment)}: {Comment}. " +
            $"{nameof(Sku)}: {Sku}. " +
            $"{nameof(DescriptionOptional)}: {DescriptionOptional}. " +
            $"{nameof(GuidMercury)}: {GuidMercury}. " +
            $"{nameof(Temperature)}: {Temperature}. " +
            $"{nameof(ProductShelfLife)}: {ProductShelfLife}. " +
            $"{nameof(Brand)}: {Brand}. " +
            $"{nameof(NameFull)}: {NameFull}. " +
            $"{nameof(AdditionalDescriptionOfNomenclature)}: {AdditionalDescriptionOfNomenclature}. " +
            $"{nameof(Units)}: {strUnits}" +
            $"{nameof(Barcodes)}: {strBarcodes}" +
            $"{nameof(Boxes)}: {strBoxes}" +
            $"{nameof(Packs)}: {strPacks}";
    }

    public bool Equals(XmlProductModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        if (Units.Count != item.Units.Count)
	        return false;
        for (int i = 0; i < Units.Count; i++)
        {
	        if (!Units[i].Equals(item.Units[i]))
		        return false;
        }
        return
            string.Equals(Category, item.Category, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Code, item.Code, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Description, item.Description, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Comment, item.Comment, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Sku, item.Sku, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(DescriptionOptional, item.DescriptionOptional, StringComparison.InvariantCultureIgnoreCase) &&
            Equals(GuidMercury, item.GuidMercury) &&
            string.Equals(Temperature, item.Temperature, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(ProductShelfLife, item.ProductShelfLife, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(Brand, item.Brand, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(NameFull, item.NameFull, StringComparison.InvariantCultureIgnoreCase) &&
            string.Equals(AdditionalDescriptionOfNomenclature, item.AdditionalDescriptionOfNomenclature, StringComparison.InvariantCultureIgnoreCase);
    }

    public bool EqualsNew()
    {
        return Equals(new());
    }

    public object Clone()
    {
	    XmlProductModel item = new();
	    item.Category = Category;
	    item.Code = Code;
	    item.Description = Description;
	    item.Comment = Comment;
	    item.Sku = Sku;
	    item.DescriptionOptional = DescriptionOptional;
	    item.GuidMercury = GuidMercury;
	    item.Temperature = Temperature;
	    item.ProductShelfLife = ProductShelfLife;
		item.Brand = Brand;
		item.Units = Units;
		item.Barcodes = Barcodes;
		item.Boxes = Boxes;
		item.Packs = Packs;
		item.NameFull = NameFull;
		item.AdditionalDescriptionOfNomenclature = AdditionalDescriptionOfNomenclature;
	    return item;
    }

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public void GetObjectData(SerializationInfo info, StreamingContext context)
    {
	    info.AddValue(nameof(Category), Category);
	    info.AddValue(nameof(Code), Code);
	    info.AddValue(nameof(Description), Description);
	    info.AddValue(nameof(Comment), Comment);
	    info.AddValue(nameof(Sku), Sku);
	    info.AddValue(nameof(DescriptionOptional), DescriptionOptional);
	    info.AddValue(nameof(GuidMercury), GuidMercury);
	    info.AddValue(nameof(Temperature), Temperature);
	    info.AddValue(nameof(ProductShelfLife), ProductShelfLife);
	    info.AddValue(nameof(Brand), Brand);
	    info.AddValue(nameof(Units), Units);
	    info.AddValue(nameof(Barcodes), Barcodes);
	    info.AddValue(nameof(Boxes), Boxes);
	    info.AddValue(nameof(Packs), Packs);
	    info.AddValue(nameof(NameFull), NameFull);
	    info.AddValue(nameof(AdditionalDescriptionOfNomenclature), AdditionalDescriptionOfNomenclature);
	}

    public void ClearNullProperties()
    {
	    throw new NotImplementedException();
    }

    public virtual void FillProperties()
    {
	    throw new NotImplementedException();
    }

	#endregion
}
