// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "LOG_TYPES".
/// </summary>
[Serializable]
public class LogTypeModel : TableModel, ISerializable, ITableModel
{
	#region Public and private fields, properties, constructor

	[XmlElement] public virtual byte Number { get; set; }
	[XmlElement] public virtual string Icon { get; set; }

	/// <summary>
	/// Constructor.
	/// </summary>
    public LogTypeModel()
	{
		Number = 0x00;
		Icon = string.Empty;
	}

	#endregion

	#region Public and private methods

	public new virtual string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Number)}: {Number}. " +
        $"{nameof(Icon)}: {Icon}. ";

    public virtual bool Equals(LogTypeModel item)
    {
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Number, item.Number) &&
               Equals(Icon, item.Icon);
    }

	public new virtual bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((LogTypeModel)obj);
    }

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return 
	        base.EqualsDefault() &&
            Equals(Number, (byte)0x00) &&
            Equals(Icon, string.Empty);
    }

    public new virtual int GetHashCode() => base.GetHashCode();

	public new virtual object Clone()
    {
        LogTypeModel item = new();
        item.Number = Number;
        item.Icon = Icon;
		item.CloneSetup(base.CloneCast());
		return item;
    }

    public new virtual LogTypeModel CloneCast() => (LogTypeModel)Clone();

    #endregion
}
