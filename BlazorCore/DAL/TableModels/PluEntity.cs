// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using BlazorCore.Models.XML;
using System;
using System.Linq;
using System.Runtime.CompilerServices;
using BlazorCore.Models;

namespace BlazorCore.DAL.TableModels
{
    public class PluEntity : BaseIdEntity
    {
        #region Public and private fields and properties

        public virtual DateTime? CreateDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual TemplateEntity Templates { get; set; } = new TemplateEntity();
        public virtual ScaleEntity Scale { get; set; } = new ScaleEntity();
        public virtual NomenclatureEntity Nomenclature { get; set; } = new NomenclatureEntity();
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
        public virtual decimal? UpperWeightThreshold { get; set; }
        public virtual decimal? NominalWeight { get; set; }
        public virtual decimal? LowerWeightThreshold { get; set; }
        public virtual bool? CheckWeight { get; set; }
        public virtual bool Marked { get; set; }

        #endregion

        #region Public and private fields and properties - XML

        public virtual string XmlGoodsName
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "GoodsName");
                }
                return GoodsName;
            }
            set => _ = value;
        }

        public virtual string XmlGoodsFullName
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "GoodsFullName");
                }
                return GoodsFullName;
            }
            set => _ = value;
        }

        public virtual string XmlGoodsDescription
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "GoodsDescription");
                }
                return GoodsDescription;
            }
            set => _ = value;
        }

        public virtual short XmlGoodsShelfLifeDays
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    var strProductShelfLife = GetXmlValue<string>(productEntity, "ProductShelfLife");
                    var arr = strProductShelfLife.Split(" ");
                    if (arr.Length > 1)
                    {
                        short.TryParse(arr[0], out var productShelfLife2);
                        return productShelfLife2;
                    }
                    short.TryParse(strProductShelfLife, out var productShelfLife1);
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
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "GTIN");
                }
                return Gtin;
            }
            set => _ = value;
        }
        
        public virtual string XmlEan13
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "EAN13");
                }
                return Ean13;
            }
            set => _ = value;
        }

        public virtual string XmlItf14
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<string>(productEntity, "ITF14");
                }
                return Itf14;
            }
            set => _ = value;
        }

        public virtual int XmlGoodsBoxQuantly
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<int>(productEntity, "GoodsBoxQuantly");
                }
                return GoodsBoxQuantly;
            }
            set => _ = value;
        }

        public virtual decimal XmlGoodsTareWeight
        {
            get
            {
                var product = ProductHelper.Instance;
                var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
                if (productEntity != null && !productEntity.EqualsNew())
                {
                    return GetXmlValue<decimal>(productEntity, "GoodsTareWeight");
                }
                return GoodsTareWeight;
            }
            set => _ = value;
        }

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            var strTemplates = Templates != null ? Templates.Id.ToString() : "null";
            var strScale = Scale != null ? Scale.Id.ToString() : "null";
            var strNomenclature = Nomenclature != null ? Nomenclature.Id.ToString() : "null";
            var strUpperWeightThreshold = UpperWeightThreshold != null ? UpperWeightThreshold.ToString() : "null";
            var strNominalWeight = NominalWeight != null ? NominalWeight.ToString() : "null";
            var strLowerWeightThreshold = LowerWeightThreshold != null ? LowerWeightThreshold.ToString() : "null";
            var strWeightControlWithout = CheckWeight != null ? CheckWeight.ToString() : "null";
            return base.ToString() +
                   $"{nameof(CreateDate)}: {CreateDate}. " + 
                   $"{nameof(ModifiedDate)}: {ModifiedDate}. " +
                   $"{nameof(Templates)}: {strTemplates}. " +
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
                   $"{nameof(UpperWeightThreshold)}: {strUpperWeightThreshold}. " +
                   $"{nameof(NominalWeight)}: {strNominalWeight}. " +
                   $"{nameof(LowerWeightThreshold)}: {strLowerWeightThreshold}. " +
                   $"{nameof(CheckWeight)}: {strWeightControlWithout}. " + 
                   $"{nameof(Marked)}: {Marked}. ";
        }

        public virtual bool Equals(PluEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return base.Equals(entity) &&
                   Equals(CreateDate, entity.CreateDate) &&
                   Equals(ModifiedDate, entity.ModifiedDate) &&
                   Equals(Templates, entity.Templates) && 
                   Equals(Scale, entity.Scale) && 
                   Equals(Nomenclature, entity.Nomenclature) &&
                   Equals(GoodsName, entity.GoodsName) &&
                   Equals(GoodsFullName, entity.GoodsFullName) &&
                   Equals(GoodsDescription, entity.GoodsDescription) &&
                   Equals(Gtin, entity.Gtin) &&
                   Equals(Ean13, entity.Ean13) &&
                   Equals(Itf14, entity.Itf14) &&
                   Equals(GoodsShelfLifeDays, entity.GoodsShelfLifeDays) &&
                   Equals(GoodsTareWeight, entity.GoodsTareWeight) &&
                   Equals(GoodsBoxQuantly, entity.GoodsBoxQuantly) &&
                   Equals(Plu, entity.Plu) &&
                   Equals(Active, entity.Active) &&
                   Equals(UpperWeightThreshold, entity.UpperWeightThreshold) &&
                   Equals(NominalWeight, entity.NominalWeight) &&
                   Equals(LowerWeightThreshold, entity.LowerWeightThreshold) &&
                   Equals(CheckWeight, entity.CheckWeight) &&
                   Equals(Marked, entity.Marked);
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
            if (Templates != null && !Templates.EqualsDefault())
                return false;
            if (Scale != null && !Scale.EqualsDefault())
                return false;
            if (Nomenclature != null && !Nomenclature.EqualsDefault())
                return false;
            return base.EqualsDefault() && 
                   Equals(CreateDate, default(DateTime?)) && 
                   Equals(ModifiedDate, default(DateTime?)) && 
                   Equals(GoodsName, default(string)) &&
                   Equals(GoodsFullName, default(string)) &&
                   Equals(GoodsName, default(string)) &&
                   Equals(GoodsDescription, default(string)) &&
                   Equals(Gtin, default(string)) &&
                   Equals(Ean13, default(string)) &&
                   Equals(Itf14, default(string)) &&
                   Equals(GoodsShelfLifeDays, default(short)) && 
                   Equals(GoodsTareWeight, default(decimal)) &&
                   Equals(GoodsBoxQuantly, default(int)) &&
                   Equals(Plu, default(int)) &&
                   Equals(Active, default(bool)) &&
                   Equals(UpperWeightThreshold, default(decimal?)) &&
                   Equals(NominalWeight, default(decimal?)) &&
                   Equals(LowerWeightThreshold, default(decimal?)) &&
                   Equals(CheckWeight, default(bool?)) &&
                   Equals(Marked, default(bool));
        }

        public override object Clone()
        {
            return new PluEntity
            {
                Id = Id,
                CreateDate = CreateDate,
                ModifiedDate = ModifiedDate,
                Templates = (TemplateEntity)Templates?.Clone(),
                Scale = (ScaleEntity)Scale?.Clone(),
                Nomenclature = (NomenclatureEntity)Nomenclature?.Clone(),
                GoodsName = GoodsName,
                GoodsFullName = GoodsFullName,
                GoodsDescription = GoodsDescription,
                Gtin = Gtin,
                Ean13 = Ean13,
                Itf14 = Itf14,
                GoodsShelfLifeDays = GoodsShelfLifeDays,
                GoodsTareWeight = GoodsTareWeight,
                GoodsBoxQuantly = GoodsBoxQuantly,
                Plu = Plu,
                Active = Active,
                UpperWeightThreshold = UpperWeightThreshold,
                NominalWeight = NominalWeight,
                LowerWeightThreshold = LowerWeightThreshold,
                CheckWeight = CheckWeight,
                Marked = Marked,
            };
        }

        private T GetXmlValue<T>(ProductEntity productEntity, string name,
            [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string memberName = "")
        {
            try
            {
                if (Nomenclature == null || productEntity == null || string.IsNullOrEmpty(name))
                    return (T)Convert.ChangeType(string.Empty, typeof(string));
                switch (name)
                {
                    case "GoodsName":
                        return (T)Convert.ChangeType(productEntity.Description, typeof(string));
                    case "GoodsFullName":
                        return (T)Convert.ChangeType(productEntity.NameFull, typeof(string));
                    case "GoodsDescription":
                        return (T)Convert.ChangeType(productEntity.AdditionalDescriptionOfNomenclature, typeof(string));
                    case "ProductShelfLife":
                        return (T)Convert.ChangeType(productEntity.ProductShelfLifeShort, typeof(string));
                    case "GTIN":
                        if (productEntity.Barcodes != null && productEntity.Barcodes.Count > 0)
                        {
                            var barcodeGtin = productEntity.Barcodes.FirstOrDefault(
                                x => x.Type.Equals("EAN13"));
                            if (barcodeGtin != null)
                            {
                                return (T)Convert.ChangeType(barcodeGtin.Barcode, typeof(string));
                            }
                        }
                        break;
                    case "EAN13":
                        if (productEntity.Barcodes != null && productEntity.Barcodes.Count > 0)
                        {
                            var barcodeEan13 = productEntity.Barcodes.FirstOrDefault(
                                x => x.Type.Equals("EAN13"));
                            if (barcodeEan13 != null)
                            {
                                return (T)Convert.ChangeType(barcodeEan13.Barcode, typeof(string));
                            }
                        }
                        break;
                    case "ITF14":
                        if (productEntity.Barcodes != null && productEntity.Barcodes.Count > 0)
                        {
                            var barcodeItf14 = productEntity.Barcodes.FirstOrDefault(
                            x => x.Type.Equals("ITF14"));
                            if (barcodeItf14 != null)
                                return (T)Convert.ChangeType(barcodeItf14.Barcode, typeof(string));
                        }
                        break;
                    case "GoodsBoxQuantly":
                        var rate1 = productEntity.Units
                            .OrderByDescending(x => x.Rate)
                            .FirstOrDefault(x => x.Description.Equals("Кор"))?
                            .Rate;
                        if (decimal.TryParse(rate1.ToString(), out var goodsBoxQuantly2))
                            return (T)Convert.ChangeType((int)goodsBoxQuantly2, typeof(int));
                        break;
                    case "GoodsTareWeight":
                        var rate = productEntity.Units
                            .OrderByDescending(x => x.Rate)
                            .FirstOrDefault(x => x.Description.Equals("Кор"))?
                            .Rate;
                        if (decimal.TryParse(rate.ToString(), out var rate2))
                        {
                            var boxHeft = productEntity.Boxes.FirstOrDefault(
                                x => x.Unit.Equals("шт"))?.Heft;
                            if (decimal.TryParse(boxHeft.ToString(), out var boxHeft2))
                            {
                                var packHeft = productEntity.Packs.FirstOrDefault(
                                    x => x.Unit.Equals("шт"))?.Heft;
                                if (decimal.TryParse(packHeft.ToString(), out var packHeft2))
                                {
                                    var brutto = packHeft2 * rate2 + boxHeft2;
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

        public virtual decimal CalcGoodsTareWeight()
        {
            var product = ProductHelper.Instance;
            var productEntity = product.GetProductEntity(Nomenclature?.SerializedRepresentationObject);
            if (productEntity != null && !productEntity.EqualsNew() && Nomenclature != null)
            {
                // Вес коробки.
                var weightBoxSource = productEntity.Boxes
                    .OrderByDescending(x => x.Heft)
                    .FirstOrDefault(x => x.Unit.Equals("шт"))?
                    .Heft;
                decimal.TryParse(weightBoxSource.ToString(), out var weightBox);

                // Вес пакета.
                var weightPackSource = productEntity.Packs
                    .OrderByDescending(x => x.Heft)
                    .FirstOrDefault(x => x.Unit.Equals("шт"))?
                    .Heft;
                decimal.TryParse(weightPackSource.ToString(), out var weightPack);

                // Кол-во вложений.
                var rateUnitSource = productEntity.Units
                    .OrderByDescending(x => x.Rate)
                    .FirstOrDefault(x => x.Description.Equals("Кор"))?
                    .Rate;
                decimal.TryParse(rateUnitSource.ToString(), out var rateUnit);

                // Вес клипсы.
                decimal weightClip = 0M;

                // вес тары = вес коробки + (вес пакета * кол. вложений) + (вес клипсы * кол. вложений * 2).
                var result = weightBox + weightPack * rateUnit + weightClip * rateUnit * 2;
                decimal.TryParse($"{result:F3}", out var resultF3);
                //Console.WriteLine("вес тары = вес коробки + (вес пакета * кол. вложений) + (вес клипсы * кол. вложений * 2)");
                //Console.WriteLine($"{resultF3} = {weightBox} + ({weightPack} * {rateUnit}) + {weightKlipsa} * {rateUnit} * 2)");
                return resultF3;
            }
            return 0;
        }

        public virtual bool GtinCheck()
        {
            if (Gtin?.Length > 12)
            {
                var barcode = BarcodeHelper.Instance;
                var gtin = barcode.GetGtin(Gtin.Substring(0, 13));
                return Equals(Gtin, gtin);
            }
            return false;
        }
        
        #endregion
    }
}
