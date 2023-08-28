namespace WsStorageCore.Xml;

public class WsXmlProductHelper
{
    #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    private static WsXmlProductHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public static WsXmlProductHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Public and private fields, properties, constructor

	private WsSqlBarCodeController Barcode { get; set; } = WsSqlBarCodeController.Instance;

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

    public List<WsXmlProductUnitModel> GetProductUnitEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<WsXmlProductUnitModel>? entities = new();
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
                        WsXmlProductUnitModel? item = new()
                        {
                            Heft = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                            Capacity = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                            Rate = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
                            Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                            Okei = GetAttribute<string>(xmlChild, "OKEI") ?? string.Empty,
                            Description = GetAttribute<string>(xmlChild, "Description") ?? string.Empty
                        };
                        entities.Add(item);
                    }
                }
            }
        }
        return entities;
    }

    public List<WsXmlProductBarcodeModel> GetProductBarcodeEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<WsXmlProductBarcodeModel>? entities = new();
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
                        WsXmlProductBarcodeModel? item = new()
                        {
                            Type = GetAttribute<string>(xmlChild, "Type") ?? string.Empty,
                            Barcode = GetAttribute<string>(xmlChild, "Barcode") ?? string.Empty
                        };
                        entities.Add(item);
                    }
                }
            }
        }
        return entities;
    }

    public List<WsXmlProductBoxModel> GetProductBoxEntities(XElement xmlElement, string nameSection, string nameElement)
    {
        List<WsXmlProductBoxModel>? items = new();
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
	                    WsXmlProductBoxModel? item = new()
                        {
                            Description = GetAttribute<string>(xmlChild, "Description") ?? string.Empty,
                            Heft = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                            Capacity = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                            Rate = WsStrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
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

    public WsXmlProductModel GetXmlProduct(string? value)
    {
        WsXmlProductModel productEntity = new();
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

	public T GetXmlValue<T>(WsXmlProductModel xmlProduct, string name,
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
						WsXmlProductBarcodeModel barcodeGtin = xmlProduct.Barcodes.FirstOrDefault(
							item => item.Type.Equals("EAN13"));
						if (barcodeGtin is not null)
						{
							return (T)Convert.ChangeType(barcodeGtin.Barcode, typeof(string));
						}
					}
					break;
				case "EAN13":
					if (xmlProduct.Barcodes is not null && xmlProduct.Barcodes.Count > 0)
					{
						WsXmlProductBarcodeModel barcodeEan13 = xmlProduct.Barcodes.FirstOrDefault(
							item => item.Type.Equals("EAN13"));
						if (barcodeEan13 is not null)
						{
							return (T)Convert.ChangeType(barcodeEan13.Barcode, typeof(string));
						}
					}
					break;
				case "ITF14":
					if (xmlProduct.Barcodes is not null && xmlProduct.Barcodes.Count > 0)
					{
						WsXmlProductBarcodeModel barcodeItf14 = xmlProduct.Barcodes.FirstOrDefault(
						item => item.Type.Equals("ITF14"));
						if (barcodeItf14 is not null)
							return (T)Convert.ChangeType(barcodeItf14.Barcode, typeof(string));
					}
					break;
				case "GoodsBoxQuantly":
					decimal? rate1 = xmlProduct.Units
						.OrderByDescending(item => item.Rate)
						.FirstOrDefault(item => item.Description.Equals("Кор"))?
						.Rate;
					if (decimal.TryParse(rate1.ToString(), out decimal goodsBoxQuantly2))
						return (T)Convert.ChangeType((int)goodsBoxQuantly2, typeof(int));
					break;
				case "GoodsWeightTare":
					decimal? rate = xmlProduct.Units
						.OrderByDescending(item => item.Rate)
						.FirstOrDefault(item => item.Description.Equals("Кор"))?
						.Rate;
					if (decimal.TryParse(rate.ToString(), out decimal rate2))
					{
						decimal? boxHeft = xmlProduct.Boxes.FirstOrDefault(
							item => item.Unit.Equals("шт"))?.Heft;
						if (decimal.TryParse(boxHeft.ToString(), out decimal boxHeft2))
						{
							decimal? packHeft = xmlProduct.Packs.FirstOrDefault(
								item => item.Unit.Equals("шт"))?.Heft;
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

	//public string GetXmlName(PluModel plu, string name)
 //   {
	//    XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//    if (!xmlProduct.EqualsNew())
	//    {
	//	    return GetXmlValue<string>(xmlProduct, "GoodsName");
	//    }
	//    return name;
 //   }

	//public string GetXmlFullName(PluModel plu, string fullName)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<string>(xmlProduct, "GoodsFullName");
	//	}
	//	return fullName;
	//}

	//public string GetXmlDescription(PluModel plu, string description)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<string>(xmlProduct, "GoodsDescription");
	//	}
	//	return description;
	//}

	//public byte GetXmlShelfLifeDays(PluModel plu, byte shelfLifeDays)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		string strProductShelfLife = GetXmlValue<string>(xmlProduct, "ProductShelfLife");
	//		string[] arr = strProductShelfLife.Split(' ');
	//		if (arr.Length > 1)
	//		{
	//			if (byte.TryParse(arr[0], out byte productShelfLife2))
	//				return productShelfLife2;
	//		}
	//		if (byte.TryParse(strProductShelfLife, out byte productShelfLife1))
	//			return productShelfLife1;
	//	}
	//	return shelfLifeDays;
	//}

	//public string GetXmlGtin(PluModel plu, string gtin)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<string>(xmlProduct, "GTIN");
	//	}
	//	return gtin;
	//}

	//public string GetXmlEan13(PluModel plu, string ean13)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<string>(xmlProduct, "EAN13");
	//	}
	//	return ean13;
	//}

	//public string GetXmlItf14(PluModel plu, string itf14)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<string>(xmlProduct, "ITF14");
	//	}
	//	return itf14;
	//}

	//public int GetXmlBundleCount(PluModel plu, int boxQuantly)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<int>(xmlProduct, "GoodsBoxQuantly");
	//	}
	//	return boxQuantly;
	//}

	//public decimal GetXmlWeightTare(PluModel plu, decimal weightTare)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew())
	//	{
	//		return GetXmlValue<decimal>(xmlProduct, "GoodsWeightTare");
	//	}
	//	return weightTare;
	//}

	/// <summary>
	/// Вес коробки.
	/// </summary>
	/// <returns></returns>
	public decimal CalcGoodWeightBox(WsSqlPluModel plu, WsXmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !plu.EqualsNew())
		{
			decimal? weightBoxSource = xmlProduct.Boxes
				.OrderByDescending(item => item.Heft)
				.FirstOrDefault(item => item.Unit.Equals("шт"))?
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
	public decimal CalcGoodWeightPack(WsSqlPluModel plu, WsXmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !plu.EqualsNew())
		{
			decimal? weightPackSource = xmlProduct.Packs
				.OrderByDescending(item => item.Heft)
				.FirstOrDefault(item => item.Unit.Equals("шт"))?
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
	public decimal CalcGoodRateUnit(WsSqlPluModel plu, WsXmlProductModel xmlProduct)
	{
		if (!xmlProduct.EqualsNew() && !plu.EqualsNew())
		{
			decimal? rateUnitSource = xmlProduct.Units
				.OrderByDescending(item => item.Rate)
				.FirstOrDefault(item => item.Description.Equals("Кор"))?
				.Rate;
			if (decimal.TryParse(rateUnitSource.ToString(), out decimal rateUnit))
				return rateUnit;
		}
		return 0M;
	}

	//public decimal CalcGoodsWeightTare(PluModel plu)
	//{
	//	XmlProductModel xmlProduct = GetXmlProduct(plu.Xml);
	//	if (!xmlProduct.EqualsNew() && !plu.EqualsNew())
	//	{
	//		// Вес коробки.
	//		decimal weightBox = CalcGoodWeightBox(plu, xmlProduct);
	//		// Вес пакета.
	//		decimal weightPack = CalcGoodWeightPack(plu, xmlProduct);
	//		// Кол-во вложений.
	//		decimal rateUnit = CalcGoodRateUnit(plu, xmlProduct);
	//		// Вес клипсы.
	//		decimal weightClip = 0M;
	//		// Вес тары = вес коробки + (вес пакета * кол. вложений) + (вес клипсы * кол. вложений * 2).
	//		decimal result = weightBox + weightPack * rateUnit + weightClip * rateUnit * 2;
	//		if (decimal.TryParse($"{result:F3}", out decimal resultF3))
	//			return resultF3;
	//	}
	//	return 0M;
	//}

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
