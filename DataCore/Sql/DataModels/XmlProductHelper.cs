// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;

namespace DataCore.Sql.DataModels
{
    public class XmlProductHelper
    {
        #region Design pattern "Lazy Singleton"

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private static XmlProductHelper _instance;
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public static XmlProductHelper Instance => LazyInitializer.EnsureInitialized(ref _instance);

        #endregion

        #region Public and private methods

        public string? GetXmlWrapp(string? value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            string? xmlBegin = @"<?xml version=""1.0"" encoding=""utf-8""?>";
            string? xmlRootBegin = "<root>";
            string? xmlRootEnd = "</root>";
            if (value != null && !value.StartsWith(xmlRootBegin) && !value.EndsWith(xmlRootEnd))
            {
                value = xmlRootBegin + Environment.NewLine + value + Environment.NewLine + xmlRootEnd;
            }
            if (value != null && !value.StartsWith(xmlBegin))
            {
                value = xmlBegin + Environment.NewLine + value;
            }
            return value;
        }

        public List<ProductUnitEntity> GetProductUnitEntities(XElement xmlElement, string nameSection, string nameElement)
        {
            List<ProductUnitEntity>? entities = new();
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
                            ProductUnitEntity? item = new()
                            {
                                Heft = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                                Capacity = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                                Rate = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
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

        public List<ProductBarcodeEntity> GetProductBarcodeEntities(XElement xmlElement, string nameSection, string nameElement)
        {
            List<ProductBarcodeEntity>? entities = new();
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
                            ProductBarcodeEntity? item = new()
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

        public List<ProductBoxEntity> GetProductBoxEntities(XElement xmlElement, string nameSection, string nameElement)
        {
            List<ProductBoxEntity>? entities = new();
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
                            ProductBoxEntity? item = new()
                            {
                                Description = GetAttribute<string>(xmlChild, "Description") ?? string.Empty,
                                Heft = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Heft") ?? "0"),
                                Capacity = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Capacity") ?? "0"),
                                Rate = Utils.StringUtils.GetDecimalValue(GetAttribute<string>(xmlChild, "Rate") ?? "0"),
                                Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                                Okei = GetAttribute<string>(xmlChild, "OKEI") ?? string.Empty,
                                Unit = GetAttribute<string>(xmlChild, "Unit") ?? string.Empty
                            };
                            entities.Add(item);
                            Console.WriteLine($"{nameof(item)}: {item}");
                        }
                    }
                }
            }
            return entities;
        }

        public XmlProductEntity GetProductEntity(string? value)
        {
            XmlProductEntity productEntity = new();
            if (string.IsNullOrEmpty(value))
                return productEntity;

            XDocument? xmlDoc = XDocument.Parse(GetXmlWrapp(value));
            XElement? xmlRoot = xmlDoc.Element("root");
            XElement? xmlProduct = xmlRoot?.Element("Product");
            if (xmlProduct != null)
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
            List<T>? items = new();
            List<XElement>? xmlEntities = xmlElement.Elements(nameElement).ToList();
            if (xmlEntities.Any())
            {
                foreach (XElement? xmlEntity in xmlEntities)
                {
                    T? item = GetAttribute<T>(xmlEntity, string.Empty);
                    if (item != null)
                        items.Add(item);
                }
            }
            return items;
        }

        #endregion
    }
}
