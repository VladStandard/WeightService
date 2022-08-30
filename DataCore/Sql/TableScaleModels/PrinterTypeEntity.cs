// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "ZebraPrinterType".
/// </summary>
[Serializable]
public class PrinterTypeEntity : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public PrinterTypeEntity() : base(ColumnName.Id, 0, false)
	{
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public PrinterTypeEntity(long identityId, bool isSetupDates) : base(ColumnName.Id, identityId, isSetupDates)
	{
		Init();
	}

	#endregion

	#region Public and private methods

	public new virtual void Init()
	{
		base.Init();
		Name = string.Empty;
	}

	public override string ToString() =>
		$"{nameof(IdentityId)}: {IdentityId}. " +
		$"{nameof(IsMarked)}: {IsMarked}. " +
		$"{nameof(Name)}: {Name}. ";

	public virtual bool Equals(PrinterTypeEntity item)
	{
		if (item is null) return false;
		if (ReferenceEquals(this, item)) return true;
		return base.Equals(item) &&
			   Equals(Name, item.Name);
	}

	public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
		return Equals((PrinterTypeEntity)obj);
	}

	public virtual bool EqualsNew()
	{
		return Equals(new());
	}

	public new virtual bool EqualsDefault()
	{
		return base.EqualsDefault() &&
			   Equals(Name, string.Empty);
	}

	public new virtual object Clone()
	{
		PrinterTypeEntity item = new();
		item.Name = Name;
		item.Setup(((TableModel)this).CloneCast());
		return item;
	}

	public new virtual PrinterTypeEntity CloneCast() => (PrinterTypeEntity)Clone();

	#endregion
}
