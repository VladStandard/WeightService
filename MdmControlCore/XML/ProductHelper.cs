// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace MdmControlCore.XML;

public class ProductHelper
{
	#region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	private static ProductHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
	public static ProductHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

	#endregion

	#region Constructor and destructor

	private ProductHelper() { }

	#endregion

	#region Public and private methods

	public static string GetXmlWrapp(string value)
	{
		string xmlBegin = @"<?xml version=""1.0"" encoding=""utf-8""?>";
		string xmlRootBegin = "<root>";
		string xmlRootEnd = "</root>";
		if (!value.StartsWith(xmlRootBegin) && !value.EndsWith(xmlRootEnd))
		{
			value = xmlRootBegin + Environment.NewLine + value + Environment.NewLine + xmlRootEnd;
		}
		if (!value.StartsWith(xmlBegin))
		{
			value = xmlBegin + Environment.NewLine + value;
		}
		return value;
	}

	public List<ProductUnitEntity> GetProductUnitEntities(XElement xmlElement, string nameSection, string nameElement)
	{
		List<ProductUnitEntity> entities = new List<ProductUnitEntity>();
		List<XElement> xmlEntities = xmlElement.Elements(nameSection).ToList();
		if (xmlEntities.Any())
		{
			foreach (XElement xmlEntity in xmlEntities)
			{
				List<XElement> xmlChilds = xmlEntity.Elements(nameElement).ToList();
				if (xmlChilds.Any())
				{
					foreach (XElement xmlChild in xmlChilds)
					{
						ProductUnitEntity item = new ProductUnitEntity
						{
							Heft = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft")),
							Capacity = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity")),
							Rate = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate")),
							Threshold = GetAttribute<int>(xmlChild, "Threshold"),
							Okei = GetAttribute<string>(xmlChild, "OKEI"),
							Description = GetAttribute<string>(xmlChild, "Description")
						};
						entities.Add(item);
					}
				}
			}
		}
		return entities;
	}

	public List<ProductBarcodeEntity> GetProductBarcodeEntities(XElement xmlElement, string nameSection, string nameElement)
	{
		List<ProductBarcodeEntity> entities = new List<ProductBarcodeEntity>();
		List<XElement> xmlEntities = xmlElement.Elements(nameSection).ToList();
		if (xmlEntities.Any())
		{
			foreach (XElement xmlEntity in xmlEntities)
			{
				List<XElement> xmlChilds = xmlEntity.Elements(nameElement).ToList();
				if (xmlChilds.Any())
				{
					foreach (XElement xmlChild in xmlChilds)
					{
						ProductBarcodeEntity item = new ProductBarcodeEntity
						{
							Type = GetAttribute<string>(xmlChild, "Type"),
							Barcode = GetAttribute<string>(xmlChild, "Barcode"),
						};
						entities.Add(item);
					}
				}
			}
		}
		return entities;
	}

	public List<ProductBoxEntity> GetProductBoxEntities(XElement xmlElement, string nameSection, string nameElement)
	{
		List<ProductBoxEntity> entities = new List<ProductBoxEntity>();
		List<XElement> xmlEntities = xmlElement.Elements(nameSection).ToList();
		if (xmlEntities.Any())
		{
			foreach (XElement xmlEntity in xmlEntities)
			{
				List<XElement> xmlChilds = xmlEntity.Elements(nameElement).ToList();
				if (xmlChilds.Any())
				{
					foreach (XElement xmlChild in xmlChilds)
					{
						ProductBoxEntity item = new ProductBoxEntity
						{
							Description = GetAttribute<string>(xmlChild, "Description"),
							Heft = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft")),
							Capacity = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity")),
							Rate = DataCore.Utils.StrUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate")),
							Threshold = GetAttribute<int>(xmlChild, "Threshold"),
							Okei = GetAttribute<string>(xmlChild, "OKEI"),
							Unit = GetAttribute<string>(xmlChild, "Unit")
						};
						entities.Add(item);
					}
				}
			}
		}
		return entities;
	}

	public ProductEntity GetXmlProduct(string value)
	{
		ProductEntity productEntity = new ProductEntity();
		if (string.IsNullOrEmpty(value))
			return productEntity;

		XDocument xmlDoc = XDocument.Parse(GetXmlWrapp(value));
		XElement xmlRoot = xmlDoc.Element("root");
		XElement xmlProduct = xmlRoot?.Element("Product");
		if (xmlProduct != null)
		{
			if (xmlProduct.HasAttributes)
			{
				productEntity.Category = GetAttribute<string>(xmlProduct, "Category");
				productEntity.Code = GetAttribute<string>(xmlProduct, "Code");
				productEntity.Description = GetAttribute<string>(xmlProduct, "Description");
				productEntity.Comment = GetAttribute<string>(xmlProduct, "Comment");
				productEntity.Sku = GetAttribute<string>(xmlProduct, "SKU");
				productEntity.DescriptionOptional = GetAttribute<string>(xmlProduct, "DescriptionOptional");
				productEntity.GuidMercury = GetAttribute<Guid>(xmlProduct, "GUIDMercury");
				productEntity.Temperature = GetAttribute<string>(xmlProduct, "Temperature");
				productEntity.ProductShelfLife = GetAttribute<string>(xmlProduct, "ProductShelfLife");
				productEntity.Brand = GetAttribute<string>(xmlProduct, "Brand");

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

	public static T GetAttribute<T>(XElement xmlElement, string nameAttribute)
	{
		if (string.IsNullOrEmpty(nameAttribute))
		{
			if (xmlElement != null)
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
			if (xmlAttribute != null)
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
		List<T> entities = new List<T>();
		List<XElement> xmlEntities = xmlElement.Elements(nameElement).ToList();
		if (xmlEntities.Any())
		{
			foreach (XElement xmlEntity in xmlEntities)
			{
				entities.Add(GetAttribute<T>(xmlEntity, null));
			}
		}
		return entities;
	}

	#endregion
}