// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableDwhModels;

[Serializable]
public class StatusModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual string Name { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public StatusModel() : base(ColumnName.Id)
    {
	    Name = string.Empty;
    }

	#endregion

	#region Public and private methods

	public override string ToString() =>
        base.ToString() +
        $"{nameof(Name)}: {Name}. ";

    public virtual bool Equals(StatusModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((StatusModel)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
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
        StatusModel item = new();
        item.Name = Name;
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual StatusModel CloneCast() => (StatusModel)Clone();

    #endregion
}
