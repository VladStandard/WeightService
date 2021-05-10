using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace DeviceControl.Core.Models.XML
{
    public sealed class ProductHelper
    {
        #region Design pattern "Lazy Singleton"

        // ReSharper disable once InconsistentNaming
        private static readonly Lazy<ProductHelper> _instance = new Lazy<ProductHelper>(() => new ProductHelper());
        public static ProductHelper Instance => _instance.Value;

        #endregion

        #region Constructor and destructor

        private ProductHelper() { Setup(); }

        public void Setup()
        {
            // Setup methods
        }

        #endregion

        #region Public and private methods

        public string GetXmlWrapp(string value)
        {
            var xmlBegin = @"<?xml version=""1.0"" encoding=""utf-8""?>";
            var xmlRootBegin = "<root>";
            var xmlRootEnd = "</root>";
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
            var entities = new List<ProductUnitEntity>();
            var xmlEntities = xmlElement.Elements(nameSection).ToList();
            if (xmlEntities.Any())
            {
                foreach (var xmlEntity in xmlEntities)
                {
                    var xmlChilds = xmlEntity.Elements(nameElement).ToList();
                    if (xmlChilds.Any())
                    {
                        foreach (var xmlChild in xmlChilds)
                        {
                            var entity = new ProductUnitEntity
                            {
                                Heft = GetAttribute<decimal>(xmlChild, "Heft"),
                                Capacity = GetAttribute<decimal>(xmlChild, "Capacity"),
                                Rate = GetAttribute<decimal>(xmlChild, "Rate"),
                                Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                                Okei = GetAttribute<string>(xmlChild, "OKEI"),
                                Description = GetAttribute<string>(xmlChild, "Description")
                            };
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        public List<ProductBarcodeEntity> GetProductBarcodeEntities(XElement xmlElement, string nameSection, string nameElement)
        {
            var entities = new List<ProductBarcodeEntity>();
            var xmlEntities = xmlElement.Elements(nameSection).ToList();
            if (xmlEntities.Any())
            {
                foreach (var xmlEntity in xmlEntities)
                {
                    var xmlChilds = xmlEntity.Elements(nameElement).ToList();
                    if (xmlChilds.Any())
                    {
                        foreach (var xmlChild in xmlChilds)
                        {
                            var entity = new ProductBarcodeEntity
                            {
                                Type = GetAttribute<string>(xmlChild, "Type"),
                                Barcode = GetAttribute<string>(xmlChild, "Barcode"),
                            };
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        public List<ProductBoxEntity> GetProductBoxEntities(XElement xmlElement, string nameSection, string nameElement)
        {
            var entities = new List<ProductBoxEntity>();
            var xmlEntities = xmlElement.Elements(nameSection).ToList();
            if (xmlEntities.Any())
            {
                foreach (var xmlEntity in xmlEntities)
                {
                    var xmlChilds = xmlEntity.Elements(nameElement).ToList();
                    if (xmlChilds.Any())
                    {
                        foreach (var xmlChild in xmlChilds)
                        {
                            var entity = new ProductBoxEntity
                            {
                                Description = GetAttribute<string>(xmlChild, "Description"),
                                Heft = GetAttribute<decimal>(xmlChild, "Heft"),
                                Capacity = GetAttribute<decimal>(xmlChild, "Capacity"),
                                Rate = GetAttribute<decimal>(xmlChild, "Rate"),
                                Threshold = GetAttribute<int>(xmlChild, "Threshold"),
                                Okei = GetAttribute<string>(xmlChild, "OKEI"),
                                Unit = GetAttribute<string>(xmlChild, "Unit")
                            };
                            entities.Add(entity);
                        }
                    }
                }
            }
            return entities;
        }

        public ProductEntity GetProductEntity(string value)
        {
            var productEntity = new ProductEntity();
            if (string.IsNullOrEmpty(value))
                return productEntity;

            var xmlDoc = XDocument.Parse(GetXmlWrapp(value));
            var xmlRoot = xmlDoc.Element("root");
            var xmlProduct = xmlRoot?.Element("Product");
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

        public T GetAttribute<T>(XElement xmlElement, string nameAttribute)
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
                        if (decimal.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
                        {
                            return (T)Convert.ChangeType(value, typeof(decimal));
                        }
                    }
                    if (typeof(T) == typeof(int))
                    {
                        if (int.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
                        {
                            return (T)Convert.ChangeType(value, typeof(int));
                        }
                    }
                    if (typeof(T) == typeof(Guid))
                    {
                        if (Guid.TryParse(xmlElement.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
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
                        if (decimal.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
                        {
                            return (T)Convert.ChangeType(value, typeof(decimal));
                        }
                    }
                    if (typeof(T) == typeof(int))
                    {
                        if (int.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
                        {
                            return (T)Convert.ChangeType(value, typeof(int));
                        }
                    }
                    if (typeof(T) == typeof(Guid))
                    {
                        if (Guid.TryParse(xmlAttribute.Value.TrimStart(' ').TrimEnd(' ').Replace(".", ","), out var value))
                        {
                            return (T)Convert.ChangeType(value, typeof(Guid));
                        }
                    }
                }
            }
            return default(T);
        }

        public List<T> GetElementValue<T>(XElement xmlElement, string nameElement)
        {
            var entities = new List<T>();
            var xmlEntities = xmlElement.Elements(nameElement).ToList();
            if (xmlEntities.Any())
            {
                foreach (var xmlEntity in xmlEntities)
                {
                    entities.Add(GetAttribute<T>(xmlEntity, null));
                }
            }
            return entities;
        }

        #endregion
    }
}
