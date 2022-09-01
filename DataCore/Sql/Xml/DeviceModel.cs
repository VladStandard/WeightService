// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.Xml;

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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    private DeviceModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Scale = (ScaleModel)info.GetValue(nameof(Scale), typeof(ScaleModel));
    }

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(Scale)}: {Scale}.";

    public new virtual int GetHashCode() => Scale.GetHashCode();

	public new virtual bool Equals(object obj)
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
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual DeviceModel CloneCast() => (DeviceModel)Clone();

    /// <summary>
    /// Get object data for serialization info.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
		base.GetObjectData(info, context);
        info.AddValue(nameof(Scale), Scale);
    }

    #endregion
}
