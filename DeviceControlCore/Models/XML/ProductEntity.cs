using System;
using System.Collections.Generic;

namespace DeviceControlCore.Models.XML
{
    /// <summary>
    /// XML-класс продукта.
    /// </summary>
    public class ProductEntity
    {
        public string Category { get; set; } = default;
        public string Code { get; set; } = default;
        public string Description { get; set; } = default;
        public string Comment { get; set; } = default;
        public string Sku { get; set; } = default;
        public string DescriptionOptional { get; set; } = default;
        public Guid GuidMercury { get; set; } = default;
        public string Temperature { get; set; } = default;
        public string ProductShelfLife { get; set; } = default;
        public short ProductShelfLifeShort 
        {
            get
            {
                if (ProductShelfLife.EndsWith(" сут.", StringComparison.InvariantCultureIgnoreCase) ||
                    ProductShelfLife.EndsWith(" сут,", StringComparison.InvariantCultureIgnoreCase))
                {
                    short.TryParse(ProductShelfLife[0..^5], out var value);
                    return value;
                }
                return 0;
            }
            set => ProductShelfLife = $"{value}  сут.";
        }
        public string Brand { get; set; } = default;
        public List<ProductUnitEntity> Units { get; set; } = new List<ProductUnitEntity>();
        public List<ProductBarcodeEntity> Barcodes { get; set; } = new List<ProductBarcodeEntity>();
        public List<ProductBoxEntity> Boxes { get; set; } = new List<ProductBoxEntity>();
        public List<ProductBoxEntity> Packs { get; set; } = new List<ProductBoxEntity>();
        public string NameFull { get; set; } = default;
        public string AdditionalDescriptionOfNomenclature { get; set; } = default;

        public override string ToString()
        {
            var strUnits = "null. ";
            if (Units != null)
            {
                strUnits = $"{Units.Count}. ";
                foreach (var unit in Units)
                {
                    strUnits += $"{unit}. ";
                }
            }
            var strBarcodes = "null. ";
            if (Barcodes != null)
            {
                strBarcodes = $"{Barcodes.Count}. ";
                foreach (var barcode in Barcodes)
                {
                    strBarcodes += $"{barcode}. ";
                }
            }
            var strBoxes = "null. ";
            if (Boxes != null)
            {
                strBoxes = $"{Boxes.Count}. ";
                foreach (var box in Boxes)
                {
                    strBoxes += $"{box}. ";
                }
            }
            var strPacks = "null. ";
            if (Packs != null)
            {
                strPacks = $"{Packs.Count}. ";
                foreach (var pack in Packs)
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

        public virtual bool Equals(ProductEntity entity)
        {
            if (entity is null) return false;
            if (ReferenceEquals(this, entity)) return true;
            if (Units != null && entity.Units != null)
            {
                if (Units.Count != entity.Units.Count)
                    return false;
                for (var i = 0; i < Units.Count; i++)
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
                Guid.Equals(GuidMercury, entity.GuidMercury) && 
                string.Equals(Temperature, entity.Temperature, StringComparison.InvariantCultureIgnoreCase) && 
                string.Equals(ProductShelfLife, entity.ProductShelfLife, StringComparison.InvariantCultureIgnoreCase) && 
                string.Equals(Brand, entity.Brand, StringComparison.InvariantCultureIgnoreCase) && 
                string.Equals(NameFull, entity.NameFull, StringComparison.InvariantCultureIgnoreCase) && 
                string.Equals(AdditionalDescriptionOfNomenclature, entity.AdditionalDescriptionOfNomenclature, StringComparison.InvariantCultureIgnoreCase);
        }
        
        public virtual bool EqualsNew()
        {
            return Equals(new ProductEntity());
        }
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
        public string Okei { get; set; }
        public string Description { get; set; }

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
        public string Type { get; set; }
        public string Barcode { get; set; }

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
        public string Description { get; set; }
        public decimal Heft { get; set; }
        public decimal Capacity { get; set; }
        public decimal Rate { get; set; }
        public int Threshold { get; set; }
        public string Okei { get; set; }
        public string Unit { get; set; }

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
