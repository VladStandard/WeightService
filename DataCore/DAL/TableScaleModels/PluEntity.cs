// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.DAL.DataModels;
using DataCore.DAL.Models;
using System;
using System.Linq;
using System.Runtime.CompilerServices;

namespace DataCore.DAL.TableScaleModels
{
    /// <summary>
    /// Table "PLUs".
    /// </summary>
    public class PluEntity : BaseEntity
    {
        #region Public and private fields and properties

        public virtual TemplateEntity Template { get; set; }
        public virtual ScaleEntity Scale { get; set; }
        public virtual NomenclatureEntity Nomenclature { get; set; }
        public virtual string GoodsName { get; set; }
        public virtual string GoodsFullName { get; set; }
        public virtual string GoodsDescription { get; set; }
        public virtual string Gtin { get; set; }
        public virtual string Ean13 { get; set; }
        public virtual string Itf14 { get; set; }
        public virtual short GoodsShelfLifeDays { get; set; }
        public virtual decimal GoodsTareWeight { get; set; }
        public virtual int GoodsBoxQuantly { get; set; }
        public virtual int Plu { get; set; }
        public virtual bool Active { get; set; }
        public virtual decimal UpperWeightThreshold { get; set; }
        public virtual decimal NominalWeight { get; set; }
        public virtual decimal LowerWeightThreshold { get; set; }
        public virtual bool CheckWeight { get; set; }

        #endregion

        #region Public and private fields and properties - Helpers

        private XmlProductHelper ProductHelper { get; set; } = XmlProductHelper.Instance;
        private BarcodeHelper Barcode { get; set; } = BarcodeHelper.Instance;

        #endregion

        #region Constructor and destructor

        public PluEntity() : this(0)
        {
            //
        }

        public PluEntity(long id) : base(id)
        {
            Template = new();
            Scale = new ScaleEntity();
            Nomenclature = new NomenclatureEntity();
            GoodsName = string.Empty;
            GoodsFullName = string.Empty;
            GoodsDescription = string.Empty;
            Gtin = string.Empty;
            Ean13 = string.Empty;
            Itf14 = string.Empty;
            GoodsShelfLifeDays = 0;
            GoodsTareWeight = 0;
            GoodsBoxQuantly = 0;
            Plu = 0;
            Active = false;
            UpperWeightThreshold = 0;
            NominalWeight = 0;
            LowerWeightThreshold = 0;
            CheckWeight = false;
        }

        #endregion

        #region Public and private fields and properties - XML

        public virtual string XmlGoodsName
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "GoodsName");
                }
                return GoodsName;
            }
            set => _ = value;
        }

        public virtual string XmlGoodsFullName
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "GoodsFullName");
                }
                return GoodsFullName;
            }
            set => _ = value;
        }

        public virtual string XmlGoodsDescription
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "GoodsDescription");
                }
                return GoodsDescription;
            }
            set => _ = value;
        }

        public virtual short XmlGoodsShelfLifeDays
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
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
                return GoodsShelfLifeDays;
            }
            set => _ = value;
        }

        public virtual string XmlGtin
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "GTIN");
                }
                return Gtin;
            }
            set => _ = value;
        }

        public virtual string XmlEan13
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "EAN13");
                }
                return Ean13;
            }
            set => _ = value;
        }

        public virtual string XmlItf14
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<string>(xmlProduct, "ITF14");
                }
                return Itf14;
            }
            set => _ = value;
        }

        public virtual int XmlGoodsBoxQuantly
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<int>(xmlProduct, "GoodsBoxQuantly");
                }
                return GoodsBoxQuantly;
            }
            set => _ = value;
        }

        public virtual decimal XmlGoodsTareWeight
        {
            get
            {
                XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (xmlProduct != null && !xmlProduct.EqualsNew())
                {
                    return GetXmlValue<decimal>(xmlProduct, "GoodsTareWeight");
                }
                return GoodsTareWeight;
            }
            set => _ = value;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string strTemplates = Template != null ? Template.IdentityId.ToString() : "null";
            string strScale = Scale != null ? Scale.IdentityId.ToString() : "null";
            string strNomenclature = Nomenclature != null ? Nomenclature.IdentityId.ToString() : "null";
            return base.ToString() +
                   $"{nameof(Template)}: {strTemplates}. " +
                   $"{nameof(Scale)}: {strScale}. " +
                   $"{nameof(Nomenclature)}: {strNomenclature}. " +
                   $"{nameof(GoodsName)}: {GoodsName}. " +
                   $"{nameof(GoodsFullName)}: {GoodsFullName}. " +
                   $"{nameof(GoodsDescription)}: {GoodsDescription}. " +
                   $"{nameof(Gtin)}: {Gtin}. " +
                   $"{nameof(Ean13)}: {Ean13}. " +
                   $"{nameof(Itf14)}: {Itf14}. " +
                   $"{nameof(GoodsShelfLifeDays)}: {GoodsShelfLifeDays}. " +
                   $"{nameof(GoodsTareWeight)}: {GoodsTareWeight}. " +
                   $"{nameof(GoodsBoxQuantly)}: {GoodsBoxQuantly}. " +
                   $"{nameof(Plu)}: {Plu}. " +
                   $"{nameof(Active)}: {Active}. " +
                   $"{nameof(UpperWeightThreshold)}: {UpperWeightThreshold}. " +
                   $"{nameof(NominalWeight)}: {NominalWeight}. " +
                   $"{nameof(LowerWeightThreshold)}: {LowerWeightThreshold}. " +
                   $"{nameof(CheckWeight)}: {CheckWeight}. ";
        }

        public virtual bool Equals(PluEntity item)
        {
            if (item is null) return false;
            if (ReferenceEquals(this, item)) return true;
            return base.Equals(item) &&
                   Equals(Template, item.Template) &&
                   Equals(Scale, item.Scale) &&
                   Equals(Nomenclature, item.Nomenclature) &&
                   Equals(GoodsName, item.GoodsName) &&
                   Equals(GoodsFullName, item.GoodsFullName) &&
                   Equals(GoodsDescription, item.GoodsDescription) &&
                   Equals(Gtin, item.Gtin) &&
                   Equals(Ean13, item.Ean13) &&
                   Equals(Itf14, item.Itf14) &&
                   Equals(GoodsShelfLifeDays, item.GoodsShelfLifeDays) &&
                   Equals(GoodsTareWeight, item.GoodsTareWeight) &&
                   Equals(GoodsBoxQuantly, item.GoodsBoxQuantly) &&
                   Equals(Plu, item.Plu) &&
                   Equals(Active, item.Active) &&
                   Equals(UpperWeightThreshold, item.UpperWeightThreshold) &&
                   Equals(NominalWeight, item.NominalWeight) &&
                   Equals(LowerWeightThreshold, item.LowerWeightThreshold) &&
                   Equals(CheckWeight, item.CheckWeight);
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((PluEntity)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public virtual bool EqualsNew()
        {
            return Equals(new PluEntity());
        }

        public new virtual bool EqualsDefault()
        {
            if (Template != null && !Template.EqualsDefault())
                return false;
            if (Scale != null && !Scale.EqualsDefault())
                return false;
            if (Nomenclature != null && !Nomenclature.EqualsDefault())
                return false;
            return base.EqualsDefault(IdentityName) &&
                   Equals(GoodsName, string.Empty) &&
                   Equals(GoodsFullName, string.Empty) &&
                   Equals(GoodsDescription, string.Empty) &&
                   Equals(Gtin, string.Empty) &&
                   Equals(Ean13, string.Empty) &&
                   Equals(Itf14, string.Empty) &&
                   Equals(GoodsShelfLifeDays, 0) &&
                   Equals(GoodsTareWeight, 0) &&
                   Equals(GoodsBoxQuantly, 0) &&
                   Equals(Plu, 0) &&
                   Equals(Active, false) &&
                   Equals(UpperWeightThreshold, 0) &&
                   Equals(NominalWeight, 0) &&
                   Equals(LowerWeightThreshold, 0) &&
                   Equals(CheckWeight, false);
        }

        public override object Clone()
        {
            PluEntity item = (PluEntity)base.Clone();
            item.Template = (TemplateEntity)Template.Clone();
            item.Scale = (ScaleEntity)Scale.Clone();
            item.Nomenclature = (NomenclatureEntity)Nomenclature.Clone();
            item.GoodsName = GoodsName;
            item.GoodsFullName = GoodsFullName;
            item.GoodsDescription = GoodsDescription;
            item.Gtin = Gtin;
            item.Ean13 = Ean13;
            item.Itf14 = Itf14;
            item.GoodsShelfLifeDays = GoodsShelfLifeDays;
            item.GoodsTareWeight = GoodsTareWeight;
            item.GoodsBoxQuantly = GoodsBoxQuantly;
            item.Plu = Plu;
            item.Active = Active;
            item.UpperWeightThreshold = UpperWeightThreshold;
            item.NominalWeight = NominalWeight;
            item.LowerWeightThreshold = LowerWeightThreshold;
            item.CheckWeight = CheckWeight;
            return item;
        }

        private T GetXmlValue<T>(XmlProductEntity xmlProduct, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                if (Nomenclature == null || xmlProduct == null || string.IsNullOrEmpty(name))
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
                        if (xmlProduct.Barcodes != null && xmlProduct.Barcodes.Count > 0)
                        {
                            ProductBarcodeEntity barcodeGtin = xmlProduct.Barcodes.FirstOrDefault(
                                x => x.Type.Equals("EAN13"));
                            if (barcodeGtin != null)
                            {
                                return (T)Convert.ChangeType(barcodeGtin.Barcode, typeof(string));
                            }
                        }
                        break;
                    case "EAN13":
                        if (xmlProduct.Barcodes != null && xmlProduct.Barcodes.Count > 0)
                        {
                            ProductBarcodeEntity barcodeEan13 = xmlProduct.Barcodes.FirstOrDefault(
                                x => x.Type.Equals("EAN13"));
                            if (barcodeEan13 != null)
                            {
                                return (T)Convert.ChangeType(barcodeEan13.Barcode, typeof(string));
                            }
                        }
                        break;
                    case "ITF14":
                        if (xmlProduct.Barcodes != null && xmlProduct.Barcodes.Count > 0)
                        {
                            ProductBarcodeEntity barcodeItf14 = xmlProduct.Barcodes.FirstOrDefault(
                            x => x.Type.Equals("ITF14"));
                            if (barcodeItf14 != null)
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
                    case "GoodsTareWeight":
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

        /// <summary>
        /// Вес коробки.
        /// </summary>
        /// <returns></returns>
        public virtual decimal CalcGoodWeightBox(XmlProductEntity xmlProduct)
        {
            if (!xmlProduct.EqualsNew() && !Nomenclature.EqualsNew())
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
        public virtual decimal CalcGoodWeightPack(XmlProductEntity xmlProduct)
        {
            if (!xmlProduct.EqualsNew() && !Nomenclature.EqualsNew())
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
        public virtual decimal CalcGoodRateUnit(XmlProductEntity xmlProduct)
        {
            if (!xmlProduct.EqualsNew() && !Nomenclature.EqualsNew())
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

        public virtual decimal CalcGoodsTareWeight()
        {
            XmlProductEntity xmlProduct = ProductHelper.GetProductEntity(Nomenclature.SerializedRepresentationObject);
            if (!xmlProduct.EqualsNew() && !Nomenclature.EqualsNew())
            {
                // Вес коробки.
                decimal weightBox = CalcGoodWeightBox(xmlProduct);
                // Вес пакета.
                decimal weightPack = CalcGoodWeightPack(xmlProduct);
                // Кол-во вложений.
                decimal rateUnit = CalcGoodRateUnit(xmlProduct);
                // Вес клипсы.
                decimal weightClip = 0M;
                // Вес тары = вес коробки + (вес пакета * кол. вложений) + (вес клипсы * кол. вложений * 2).
                decimal result = weightBox + weightPack * rateUnit + weightClip * rateUnit * 2;
                if (decimal.TryParse($"{result:F3}", out decimal resultF3))
                    return resultF3;
            }
            return 0M;
        }

        public virtual bool GtinCheck()
        {
            if (Gtin?.Length > 12)
            {
                string gtin = Barcode.GetGtin(Gtin[..13]);
                return Equals(Gtin, gtin);
            }
            return false;
        }

        #endregion
    }
}
