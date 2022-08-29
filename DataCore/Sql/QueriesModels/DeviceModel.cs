// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.QueriesModels;

[Serializable]
public class DeviceModel : TableModel, ISerializable, ITableModel
{
    #region Public and private fields, properties, constructor

    public virtual ScaleEntity Scales { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
	public DeviceModel() : base(0, false)
    {
		Init();
	}

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="identityId"></param>
	/// <param name="isSetupDates"></param>
	public DeviceModel(long identityId, bool isSetupDates) : base(identityId, isSetupDates)
	{
		Init();
	}

    public void Init()
    {
        Scales = new();
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Scales)}: {Scales}.";

    public override int GetHashCode()
    {
        return Scales.GetHashCode();
    }

    public override bool Equals(object obj)
    {
	    if (ReferenceEquals(null, obj)) return false;
	    if (ReferenceEquals(this, obj)) return true;
		if (obj is DeviceModel item)
        {
            return
               Scales == null && item.Scales == null || Scales != null && Scales.Equals(item.Scales);
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
        if (Scales != null && !Scales.EqualsDefault())
            return false;
        return base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        DeviceModel item = new()
        {
            Scales = Scales.CloneCast(),
        };
        item.Setup(((TableModel)this).CloneCast());
        return item;
    }

    public new virtual DeviceModel CloneCast() => (DeviceModel)Clone();

    #endregion
}
