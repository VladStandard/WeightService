// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleFkModels.PlusBundlesFks;
using DataCore.Sql.TableScaleModels.Nomenclatures;
using DataCore.Sql.TableScaleModels.Plus;

namespace DataCore.Sql.Xml;

public class XmlProductHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static XmlProductHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static XmlProductHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	private BarcodeHelper Barcode { get; set; } = BarcodeHelper.Instance;

	#endregion

	#region Public and private methods

	public string? GetXmlWrapp(string? value)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        string? xmlBegin = @"<?xml version=""1.0"" encoding=""utf-8""?>";
        string? xmlRootBegin = "<root>";
        string? xmlRootEnd = "</root>";
        if (value is not null && !value.StartsWith(xmlRootBegin) && !value.EndsWith(xmlRootEnd))
        {
            value = xmlRootBegin + Environment.NewLine + value + Environment.NewLine + xmlRootEnd;
        }
        if (value is not null && !value.StartsWith(xmlBegin))
        {
            value = xmlBegin + Environment.NewLine + value;
        }
        return value;
    }

    public List<XmlProductUnitModel> GetProductUnitEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<XmlProductUnitModel>? entities = new();
        List<XElement>? xmlEntities = xmlElement.Elements(nameSection).ToList();
        if (xmlEntities.Any())
        {
            foreach (XElement? xmlEntity in xmlEntities)
            {
                List<XElement>? xmlChilds = xmlEntity.Elements(nameElement).ToList();
                if (xmlChilds.Any())
                {
                    foreach (XElement? xmlChild in xmlChilds)
                    {
                        XmlProductUnitModel? item = new()
                        {
                            Heft = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                            Capacity = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                            Rate = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
                            Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                            Okei = GetAttribute<string>(xmlChild, "OKEI") ?? string.Empty,
                            Description = GetAttribute<string>(xmlChild, "Description") ?? string.Empty,
                        };
                        entities.Add(item);
                    }
                }
            }
        }
        return entities;
    }

    public List<XmlProductBarcodeModel> GetProductBarcodeEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<XmlProductBarcodeModel>? entities = new();
        List<XElement>? xmlEntities = xmlElement.Elements(nameSection).ToList();
        if (xmlEntities.Any())
        {
            foreach (XElement? xmlEntity in xmlEntities)
            {
                List<XElement>? xmlChilds = xmlEntity.Elements(nameElement).ToList();
                if (xmlChilds.Any())
                {
                    foreach (XElement? xmlChild in xmlChilds)
                    {
                        XmlProductBarcodeModel? item = new()
                        {
                            Type = GetAttribute<string>(xmlChild, "Type") ?? string.Empty,
                            Barcode = GetAttribute<string>(xmlChild, "Barcode") ?? string.Empty,
                        };
                        entities.Add(item);
                    }
                }
            }
        }
        return entities;
    }

    public List<XmlProductBoxModel> GetProductBoxEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<XmlProductBoxModel>? items = new();
        List<XElement>? xmlEntities = xmlElement.Elements(nameSection).ToList();
        if (xmlEntities.Any())
        {
            foreach (XElement? xmlEntity in xmlEntities)
            {
                List<XElement>? xmlChilds = xmlEntity.Elements(nameElement).ToList();
                if (xmlChilds.Any())
                {
                    foreach (XElement? xmlChild in xmlChilds)
                    {
	                    XmlProductBoxModel? item = new()
                        {
                            Description = GetAttribute<string>(xmlChild, "Description") ?? string.Empty,
                            Heft = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                            Capacity = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                            Rate = StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
                            Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                            Okei = GetAttribute<string>(xmlChild, "OKEI") ?? string.Empty,
                            Unit = GetAttribute<string>(xmlChild, "Unit") ?? string.Empty
                        };
                        items.Add(item);
                        Console.WriteLine($"{nameof(item)}: {item}");
                    }
                }
            }
        }
        return items;
    }

    public XmlProductModel GetXmlProduct(string? value)
    {
        XmlProductModel productEntity = new();
        if (string.IsNullOrEmpty(value))
            return productEntity;

        XDocument? xmlDoc = XDocument.Parse(GetXmlWrapp(value));
        XElement? xmlRoot = xmlDoc.Element("root");
        XElement? xmlProduct = xmlRoot?.Element("Product");
        if (xmlProduct is not null)
        {
            if (xmlProduct.HasAttributes)
            {
                productEntity.Category = GetAttribute<string>(xmlProduct, "Category") ?? string.Empty;
                productEntity.Code = GetAttribute<string>(xmlProduct, "Code") ?? string.Empty;
                productEntity.Description = GetAttribute<string>(xmlProduct, "Description") ?? string.Empty;
                productEntity.Comment = GetAttribute<string>(xmlProduct, "Comment") ?? string.Empty;
                productEntity.Sku = GetAttribute<string>(xmlProduct, "SKU") ?? string.Empty;
                productEntity.DescriptionOptional = GetAttribute<string>(xmlProduct, "DescriptionOptional") ?? string.Empty;
                productEntity.GuidMercury = GetAttribute<Guid>(xmlProduct, "GUIDMercury");
                productEntity.Temperature = GetAttribute<string>(xmlProduct, "Temperature") ?? string.Empty;
                productEntity.ProductShelfLife = GetAttribute<string>(xmlProduct, "ProductShelfLife") ?? string.Empty;
                productEntity.Brand = GetAttribute<string>(xmlProduct, "Brand") ?? string.Empty;

                productEntity.Units = GetProductUnitEntities(xmlProduct, "Units", "Unit");
                productEntity.Barcodes = GetProductBarcodeEntities(xmlProduct, "Barcodes", "Barcode");
                productEntity.Boxes = GetProductBoxEntities(xmlProduct, "Box", "Box");
                productEntity.Packs = GetProductBoxEntities(xmlProduct, "Pack", "Pack");

                productEntity.NameFull = GetElementValue<string>(xmlProduct, "NameFull").FirstOrDefault();
                productEntity.AdditionalDescriptionOfNomenclature =
                    GetElementValue<string>(xmlProduct, "AdditionalDescriptionOfNomenclature").FirstOrDefault();
            }
        }
        return productEntity;
    }

    public T? GetAttribute<T>(XElement xmlElement, string nameAttribute)
    {
        if (string.IsNullOrEmpty(nameAttribute))
        {
            if (xmlElement is not null)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)Convert.ChangeType(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), typeof(string));
                }
                if (typeof(T) == typeof(decimal))
                {
                    if (decimal.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out decimal value))
                    {
                        return (T)Convert.ChangeType(value, typeof(decimal));
                    }
                }
                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out int value))
                    {
                        return (T)Convert.ChangeType(value, typeof(int));
                    }
                }
                if (typeof(T) == typeof(Guid))
                {
                    if (Guid.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out Guid value))
                    {
                        return (T)Convert.ChangeType(value, typeof(Guid));
                    }
                }
            }
        }
        else
        {
            XAttribute xmlAttribute = xmlElement.Attribute(nameAttribute);
            if (xmlAttribute is not null)
            {
                if (typeof(T) == typeof(string))
                {
                    return (T)Convert.ChangeType(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), typeof(string));
                }
                if (typeof(T) == typeof(decimal))
                {
                    if (decimal.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out decimal value))
                    {
                        return (T)Convert.ChangeType(value, typeof(decimal));
                    }
                }
                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out int value))
                    {
                        return (T)Convert.ChangeType(value, typeof(int));
                    }
                }
                if (typeof(T) == typeof(Guid))
                {
                    if (Guid.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out Guid value))
                    {
                        return (T)Convert.ChangeType(value, typeof(Guid));
                    }
                }
            }
        }
        return default;
    }

    public List<T> GetElementValue<T>(XElement xmlElement, string nameElement)
    {
        List<T>? items = new();
        List<XElement>? xmlEntities = xmlElement.Elements(nameElement).ToList();
        if (xmlEntities.Any())
        {
            foreach (XElement? xmlEntity in xmlEntities)
            {
                T? item = GetAttribute<T>(xmlEntity, string.Empty);
                if (item is not null)
                    items.Add(item);
            }
        }
        return items;
    }

	public T GetXmlValue<T>(XmlProductModel xmlProduct, string name,
	[CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
	{
		try
		{
			if (string.IsNullOrEmpty(name))
				return (T)Convert.ChangeType(string.Empty, typeof(string));
			switch (name)
			{
				case "GoodsName":
					return (T)Convert.ChangeType(xmlProduct.Description, typeof(string));
				case "GoodsFullName":
					return (T)Convert.ChangeType(xmlProduct.NameFull, typeof(string));
				case "GoodsDescription":
					return (T)Convert.ChangeType(xmlProduct.AdditionalDescriptionOfNomenclature, typeof(string));
				case "ProductShelfLife":
					return (T)Convert.ChangeType(xmlProduct.ProductShelfLifeShort, typeof(string));
				case "GTIN":
					if (xmlProduct.Barcodes is not null && xmlProduct.Barcodes.Count > 0)
					{
						XmlProductBarcodeModel barcodeGtin = xmlProduct.Barcodes.FirstOrDefault(
							x => x.Type.Equals("EAN13"));
						if (barcodeGtin is not null)
						{
							return (T)Convert.ChangeType(barcodeGtin.Barcode, typeof(string));
						}
					}
					break;
				case "EAN13":
					if (xmlProduct.Barcodes is not null && xmlProduct.Barcodes.Count > 0)
					{
						XmlProductBarcodeModel barcodeEan13 = xmlProduct.Barcodes.FirstOrDefault(
							x => x.Type.Equals("EAN13"));
						if (barcodeEan13 is not null)
						{
							return (T)Convert.ChangeType(barcodeEan13.Barcode, typeof(string));
						}
					}
					break;
				case "ITF14":
					if (xmlProduct.Barcodes is not null && xmlProduct.Barcodes.Count > 0)
					{
						XmlProductBarcodeModel barcodeItf14 = xmlProduct.Barcodes.FirstOrDefault(
						x => x.Type.Equals("ITF14"));
						if (barcodeItf14 is not null)
							return (T)Convert.ChangeType(barcodeItf14.Barcode, typeof(string));
					}
					break;
				case "GoodsBoxQuantly":
					decimal? rate1 = xmlProduct.Units
						.OrderByDescending(x => x.Rate)
						.FirstOrDefault(x => x.Description.Equals("Кор"))?
						.Rate;
					if (decimal.TryParse(rate1.ToString(), out decimal goodsBoxQuantly2))
						return (T)Convert.ChangeType((int)goodsBoxQuantly2, typeof(int));
					break;
				case "GoodsWeightTare":
					decimal? rate = xmlProduct.Units
						.OrderByDescending(x => x.Rate)
						.FirstOrDefault(x => x.Description.Equals("Кор"))?
						.Rate;
					if (decimal.TryParse(rate.ToString(), out decimal rate2))
					{
						decimal? boxHeft = xmlProduct.Boxes.FirstOrDefault(
							x => x.Unit.Equals("шт"))?.Heft;
						if (decimal.TryParse(boxHeft.ToString(), out decimal boxHeft2))
						{
							decimal? packHeft = xmlProduct.Packs.FirstOrDefault(
								x => x.Unit.Equals("шт"))?.Heft;
							if (decimal.TryParse(packHeft.ToString(), out decimal packHeft2))
							{
								decimal brutto = packHeft2 * rate2 + boxHeft2;
								return (T)Convert.ChangeType(Convert.ToDecimal(Convert.ToString($"{brutto:F3}")), typeof(decimal));
							}
						}
					}
					break;
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"{nameof(filePath)}: {filePath}. {nameof(lineNumber)}: {lineNumber}. {nameof(memberName)}: {memberName}.");
			Console.WriteLine($"{ex.Message}");
		}
		return (T)Convert.ChangeType(string.Empty, typeof(string));
	}

	public string GetXmlName(NomenclatureModel nomenclature, string name)
    {
	    XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
	    if (!xmlProduct.EqualsNew())
	    {
		    return GetXmlValue<string>(xmlProduct, "GoodsName");
	    }
	    return name;
    }

	public string GetXmlFullName(NomenclatureModel nomenclature, string fullName)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<string>(xmlProduct, "GoodsFullName");
		}
		return fullName;
	}

	public string GetXmlDescription(NomenclatureModel nomenclature, string description)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<string>(xmlProduct, "GoodsDescription");
		}
		return description;
	}

	public short GetXmlShelfLifeDays(NomenclatureModel nomenclature, short shelfLifeDays)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			string strProductShelfLife = GetXmlValue<string>(xmlProduct, "ProductShelfLife");
			string[] arr = strProductShelfLife.Split(' ');
			if (arr.Length > 1)
			{
				if (short.TryParse(arr[0], out short productShelfLife2))
					return productShelfLife2;
			}
			if (short.TryParse(strProductShelfLife, out short productShelfLife1))
				return productShelfLife1;
		}
		return shelfLifeDays;
	}

	public string GetXmlGtin(NomenclatureModel nomenclature, string gtin)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<string>(xmlProduct, "GTIN");
		}
		return gtin;
	}

	public string GetXmlEan13(NomenclatureModel nomenclature, string ean13)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<string>(xmlProduct, "EAN13");
		}
		return ean13;
	}

	public string GetXmlItf14(NomenclatureModel nomenclature, string itf14)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<string>(xmlProduct, "ITF14");
		}
		return itf14;
	}

	public int GetXmlBoxQuantly(NomenclatureModel nomenclature, int boxQuantly)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<int>(xmlProduct, "GoodsBoxQuantly");
		}
		return boxQuantly;
	}

	public decimal GetXmlWeightTare(NomenclatureModel nomenclature, decimal weightTare)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew())
		{
			return GetXmlValue<decimal>(xmlProduct, "GoodsWeightTare");
		}
		return weightTare;
	}

	/// <summary>
	/// Вес коробки.
	/// </summary>
	/// <returns></returns>
	public decimal CalcGoodWeightBox(NomenclatureModel nomenclature, XmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !nomenclature.EqualsNew())
		{
			decimal? weightBoxSource = xmlProduct.Boxes
				.OrderByDescending(x => x.Heft)
				.FirstOrDefault(x => x.Unit.Equals("шт"))?
				.Heft;
			if (decimal.TryParse(weightBoxSource.ToString(), out decimal weightBox))
				return weightBox;
		}
		return 0M;
	}

	/// <summary>
	/// Вес пакета.
	/// </summary>
	/// <returns></returns>
	public decimal CalcGoodWeightPack(NomenclatureModel nomenclature, XmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !nomenclature.EqualsNew())
		{
			decimal? weightPackSource = xmlProduct.Packs
				.OrderByDescending(x => x.Heft)
				.FirstOrDefault(x => x.Unit.Equals("шт"))?
				.Heft;
			if (decimal.TryParse(weightPackSource.ToString(), out decimal weightPack))
				return weightPack;
		}
		return 0M;
	}

	/// <summary>
	/// Кол-во вложений.
	/// </summary>
	/// <returns></returns>
	public decimal CalcGoodRateUnit(NomenclatureModel nomenclature, XmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !nomenclature.EqualsNew())
		{
			decimal? rateUnitSource = xmlProduct.Units
				.OrderByDescending(x => x.Rate)
				.FirstOrDefault(x => x.Description.Equals("Кор"))?
				.Rate;
			if (decimal.TryParse(rateUnitSource.ToString(), out decimal rateUnit))
				return rateUnit;
		}
		return 0M;
	}

	public decimal CalcGoodsWeightTare(NomenclatureModel nomenclature)
	{
		XmlProductModel xmlProduct = GetXmlProduct(nomenclature.Xml);
		if (!xmlProduct.EqualsNew() && !nomenclature.EqualsNew())
		{
			// Вес коробки.
			decimal weightBox = CalcGoodWeightBox(nomenclature, xmlProduct);
			// Вес пакета.
			decimal weightPack = CalcGoodWeightPack(nomenclature, xmlProduct);
			// Кол-во вложений.
			decimal rateUnit = CalcGoodRateUnit(nomenclature, xmlProduct);
			// Вес клипсы.
			decimal weightClip = 0M;
			// Вес тары = вес коробки + (вес пакета * кол. вложений) + (вес клипсы * кол. вложений * 2).
			decimal result = weightBox + weightPack * rateUnit + weightClip * rateUnit * 2;
			if (decimal.TryParse($"{result:F3}", out decimal resultF3))
				return resultF3;
		}
		return 0M;
	}

	public bool GtinCheck(string gtin)
	{
		if (gtin.Length > 12)
		{
			string gtin2 = Barcode.GetGtinWithCheckDigit(gtin[..13]);
			return Equals(gtin, gtin2);
		}
		return false;
	}

  //  public string GetWeightFormula(PluModel plu, PluBundleFkModel pluBundleFk)
  //  {
  //      XmlProductModel xmlProduct = GetXmlProduct(plu.Nomenclature.Xml);
		//// Вес тары = вес пакета * кол. вложений + вес коробки
		////return $"{CalcGoodWeightBox(plu.Nomenclature, xmlProduct)} + " +
		////       $"({CalcGoodWeightPack(plu.Nomenclature, xmlProduct)} * " +
		////       $"{CalcGoodRateUnit(plu.Nomenclature, xmlProduct)})";
		//return $"{pluBundleFk.WeightTare} + " +
  //             $"({CalcGoodWeightPack(plu.Nomenclature, xmlProduct)} * " +
  //             $"{CalcGoodRateUnit(plu.Nomenclature, xmlProduct)})";
  //  }

    #endregion
}
