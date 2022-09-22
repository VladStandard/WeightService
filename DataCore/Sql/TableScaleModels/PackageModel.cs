// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PACKAGES".
/// </summary>
[Serializable]
public class PackageModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual string Name { get; set; }
    [XmlElement] public virtual decimal Weight { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PackageModel() : base(SqlFieldIdentityEnum.Uid)
	{
        Name = string.Empty;
	    Weight = default;
    }

	/// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PackageModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        Name = info.GetString(nameof(Name));
		Weight = info.GetDecimal(nameof(Weight));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() =>
		$"{nameof(IsMarked)}: {IsMarked}. " +
	    $"{nameof(Name)}: {Name}. " +
	    $"{nameof(Weight)}: {Weight}. ";

    public override bool Equals(object obj)
	{
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PackageModel)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        return
            base.EqualsDefault() &&
            Equals(Name, string.Empty) &&
            Equals(Weight, default(decimal));
    }

	public override object Clone()
    {
        PackageModel item = new();
        item.Name = Name;
        item.Weight = Weight;
        item.CloneSetup(base.CloneCast());
		return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Name), Name);
        info.AddValue(nameof(Weight), Weight);
    }

    public override void FillProperties()
    {
	    base.FillProperties();
		Name = LocaleCore.Sql.SqlItemFieldName;
		Weight = 0.560M;
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(PackageModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		return
			base.Equals(item) &&
			Equals(Name, item.Name) &&
			Equals(Weight, item.Weight);
	}

	public new virtual PackageModel CloneCast() => (PackageModel)Clone();

	#endregion
}
