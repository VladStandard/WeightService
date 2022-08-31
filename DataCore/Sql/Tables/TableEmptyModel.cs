// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.Tables;

/// <summary>
/// DB empty table model.
/// </summary>
public class TableEmptyModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Constructor.
    /// </summary>
    public TableEmptyModel() : base()
    {
        //
    }

    #endregion

    #region Public and private methods

    public virtual bool Equals(TableEmptyModel item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item);
    }

    public override bool Equals(object obj)
    {
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((TableEmptyModel)obj);
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
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        TableEmptyModel item = new();
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual TableEmptyModel CloneCast() => (TableEmptyModel)Clone();

    #endregion
}
