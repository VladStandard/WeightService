// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using System.Collections.Generic;

namespace MdmControlCore.XML;

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
				_ = short.TryParse(ProductShelfLife[0..^5], out short value);
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
		string strUnits = "null. ";
		if (Units != null)
		{
			strUnits = $"{Units.Count}. ";
			foreach (ProductUnitEntity unit in Units)
			{
				strUnits += $"{unit}. ";
			}
		}
		string strBarcodes = "null. ";
		if (Barcodes != null)
		{
			strBarcodes = $"{Barcodes.Count}. ";
			foreach (ProductBarcodeEntity barcode in Barcodes)
			{
				strBarcodes += $"{barcode}. ";
			}
		}
		string strBoxes = "null. ";
		if (Boxes != null)
		{
			strBoxes = $"{Boxes.Count}. ";
			foreach (ProductBoxEntity box in Boxes)
			{
				strBoxes += $"{box}. ";
			}
		}
		string strPacks = "null. ";
		if (Packs != null)
		{
			strPacks = $"{Packs.Count}. ";
			foreach (ProductBoxEntity pack in Packs)
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

	public virtual bool Equals(ProductEntity item)
	{
		if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		if (Units != null && item.Units != null)
		{
			if (Units.Count != item.Units.Count)
				return false;
			for (int i = 0; i < Units.Count; i++)
			{
				if (!Units[i].Equals(item.Units[i]))
					return false;
			}
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

	public virtual bool Equals(ProductUnitEntity item)
	{
		if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Heft, item.Heft) &&
			Equals(Capacity, item.Capacity) &&
			Equals(Rate, item.Rate) &&
			Equals(Threshold, item.Threshold) &&
			Equals(Okei, item.Okei) &&
			Equals(Description, item.Description);
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

	public virtual bool Equals(ProductBarcodeEntity item)
	{
		if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Type, item.Type) &&
			Equals(Barcode, item.Barcode);
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

	public virtual bool Equals(ProductBoxEntity item)
	{
		if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		return
			Equals(Description, item.Description) &&
			Equals(Heft, item.Heft) &&
			Equals(Capacity, item.Capacity) &&
			Equals(Rate, item.Rate) &&
			Equals(Threshold, item.Threshold) &&
			Equals(Okei, item.Okei) &&
			Equals(Unit, item.Unit);
	}

	public virtual bool EqualsNew()
	{
		return Equals(new ProductBoxEntity());
	}
}