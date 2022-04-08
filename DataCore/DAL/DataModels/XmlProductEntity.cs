// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace DataCore.DAL.DataModels
{
    /// <summary>
    /// XML-класс продукта.
    /// </summary>
    public class XmlProductEntity
    {
        #region Public and private fields and properties

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
        public List<ProductUnitEntity> Units { get; set; }
        public List<ProductBarcodeEntity> Barcodes { get; set; }
        public List<ProductBoxEntity> Boxes { get; set; }
        public List<ProductBoxEntity> Packs { get; set; }
        public string NameFull { get; set; }
        public string AdditionalDescriptionOfNomenclature { get; set; }

        #endregion

        #region Constructor and destructor

        public XmlProductEntity()
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

        #endregion

        #region Public and private methods

        public override string ToString()
        {
            string? strUnits = "null. ";
            if (Units != null)
            {
                strUnits = $"{Units.Count}. ";
                foreach (ProductUnitEntity? unit in Units)
                {
                    strUnits += $"{unit}. ";
                }
            }
            string? strBarcodes = "null. ";
            if (Barcodes != null)
            {
                strBarcodes = $"{Barcodes.Count}. ";
                foreach (ProductBarcodeEntity? barcode in Barcodes)
                {
                    strBarcodes += $"{barcode}. ";
                }
            }
            string? strBoxes = "null. ";
            if (Boxes != null)
            {
                strBoxes = $"{Boxes.Count}. ";
                foreach (ProductBoxEntity? box in Boxes)
                {
                    strBoxes += $"{box}. ";
                }
            }
            string? strPacks = "null. ";
            if (Packs != null)
            {
                strPacks = $"{Packs.Count}. ";
                foreach (ProductBoxEntity? pack in Packs)
                {
                    strPacks += $"{pack}. ";
                }
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

        public virtual bool Equals(XmlProductEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            if (Units != null && entity.Units != null)
            {
                if (Units.Count != entity.Units.Count)
                    return false;
                for (int i = 0; i < Units.Count; i++)
                {
                    if (!Units[i].Equals(entity.Units[i]))
                        return false;
                }
            }
            return
                string.Equals(Category, entity.Category, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Code, entity.Code, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Description, entity.Description, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Comment, entity.Comment, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Sku, entity.Sku, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(DescriptionOptional, entity.DescriptionOptional, StringComparison.InvariantCultureIgnoreCase) &&
                Equals(GuidMercury, entity.GuidMercury) &&
                string.Equals(Temperature, entity.Temperature, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(ProductShelfLife, entity.ProductShelfLife, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(Brand, entity.Brand, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(NameFull, entity.NameFull, StringComparison.InvariantCultureIgnoreCase) &&
                string.Equals(AdditionalDescriptionOfNomenclature, entity.AdditionalDescriptionOfNomenclature, StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool EqualsNew()
        {
            return Equals(new XmlProductEntity());
        }

        #endregion
    }

    /// <summary>
    /// XML-класс юнита продукта.
    /// </summary>
    public class ProductUnitEntity
    {
        public decimal Heft { get; set; }
        public decimal Capacity { get; set; }
        public decimal Rate { get; set; }
        public int Threshold { get; set; }
        public string Okei { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public override string ToString()
        {
            return
                $"{nameof(Heft)}: {Heft}. " +
                $"{nameof(Capacity)}: {Capacity}. " +
                $"{nameof(Rate)}: {Rate}. " +
                $"{nameof(Threshold)}: {Threshold}. " +
                $"{nameof(Okei)}: {Okei}. " +
                $"{nameof(Description)}: {Description}. ";
        }

        public virtual bool Equals(ProductUnitEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                Equals(Heft, entity.Heft) &&
                Equals(Capacity, entity.Capacity) &&
                Equals(Rate, entity.Rate) &&
                Equals(Threshold, entity.Threshold) &&
                Equals(Okei, entity.Okei) &&
                Equals(Description, entity.Description);
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductUnitEntity());
        }
    }

    /// <summary>
    /// XML-класс штрих-кода.
    /// </summary>
    public class ProductBarcodeEntity
    {
        public string Type { get; set; } = string.Empty;
        public string Barcode { get; set; } = string.Empty;

        public override string ToString()
        {
            return
                $"{nameof(Type)}: {Type}. " +
                $"{nameof(Barcode)}: {Barcode}. ";
        }

        public virtual bool Equals(ProductBarcodeEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                Equals(Type, entity.Type) &&
                Equals(Barcode, entity.Barcode);
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductBarcodeEntity());
        }
    }

    /// <summary>
    /// XML-класс коробки.
    /// </summary>
    public class ProductBoxEntity
    {
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Вес.
        /// </summary>
        public decimal Heft { get; set; }
        /// <summary>
        /// .
        /// </summary>
        public decimal Capacity { get; set; }
        /// <summary>
        /// .
        /// </summary>
        public decimal Rate { get; set; }
        public int Threshold { get; set; }
        public string Okei { get; set; } = string.Empty;
        public string Unit { get; set; } = string.Empty;

        public override string ToString()
        {
            return
                $"{nameof(Description)}: {Description}. " +
                $"{nameof(Heft)}: {Heft}. " +
                $"{nameof(Capacity)}: {Capacity}. " +
                $"{nameof(Rate)}: {Rate}. " +
                $"{nameof(Threshold)}: {Threshold}. " +
                $"{nameof(Okei)}: {Okei}. " +
                $"{nameof(Unit)}: {Unit}. ";
        }

        public virtual bool Equals(ProductBoxEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            return
                Equals(Description, entity.Description) &&
                Equals(Heft, entity.Heft) &&
                Equals(Capacity, entity.Capacity) &&
                Equals(Rate, entity.Rate) &&
                Equals(Threshold, entity.Threshold) &&
                Equals(Okei, entity.Okei) &&
                Equals(Unit, entity.Unit);
        }

        public virtual bool EqualsNew()
        {
            return Equals(new ProductBoxEntity());
        }
    }
}
