// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.QueriesModels;

[Serializable]
public class DeviceModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual ScaleModel Scale { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public DeviceModel() : base(ColumnName.Id)
    {
	    Scale = new();
	}

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Scale)}: {Scale}.";

    public override int GetHashCode()
    {
        return Scale.GetHashCode();
    }

    public override bool Equals(object obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
		if (obj is DeviceModel item)
        {
            return
               Scale.Equals(item.Scale);
        }
        return false;
    }

    #endregion

    #region Public and private methods

    public virtual bool EqualsNew()
    {
        return Equals(new DeviceModel());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Scale.EqualsDefault())
            return false;
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        DeviceModel item = new()
        {
            Scale = Scale.CloneCast(),
        };
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual DeviceModel CloneCast() => (DeviceModel)Clone();

    #endregion
}
