// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

using DataCore.Sql.Tables;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[Serializable]
public class PluLabelModel : SqlTableBase, ICloneable, ISqlDbBase, ISerializable
{
    #region Public and private fields, properties, constructor

    [XmlElement] public virtual PluWeighingModel PluWeighing { get; set; }
    [XmlElement] public virtual string Zpl { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelModel() : base(SqlFieldIdentityEnum.Uid)
	{
	    PluWeighing = new();
	    Zpl = string.Empty;
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluLabelModel(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluWeighing = (PluWeighingModel)info.GetValue(nameof(PluWeighing), typeof(PluWeighingModel));
        Zpl = info.GetString(nameof(Zpl));
    }

	#endregion

	#region Public and private methods - override

	public override string ToString() => base.ToString() + 
		$"{nameof(PluWeighing)}: {PluWeighing?.ToString() ?? string.Empty}. " + 
		$"{nameof(Zpl)}: {Zpl.Length}. ";

    public override bool Equals(object obj)
	{
		if (ReferenceEquals(null, obj)) return false;
		if (ReferenceEquals(this, obj)) return true;
		if (obj.GetType() != GetType()) return false;
        return Equals((PluLabelModel)obj);
    }
    
    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault()
    {
        if (!PluWeighing.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(Zpl, string.Empty);
    }

	public override object Clone()
    {
        PluLabelModel item = new();
        item.IsMarked = IsMarked;
        item.PluWeighing = PluWeighing.CloneCast();
        item.Zpl = Zpl;
        item.CloneSetup(base.CloneCast());
		return item;
    }

    public override void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluWeighing), PluWeighing);
        info.AddValue(nameof(Zpl), Zpl);
    }

    public override void FillProperties()
    {
	    base.FillProperties();
		Zpl = LocaleCore.Sql.SqlItemFieldZpl;
		//PluWeighing = new();
	}

	#endregion

	#region Public and private methods - virtual

	public virtual bool Equals(PluLabelModel item)
	{
		if (ReferenceEquals(this, item)) return true;
		if (!PluWeighing.Equals(item.PluWeighing))
			return false;
		return
			base.Equals(item) &&
			Equals(PluWeighing, item.PluWeighing) &&
			Equals(Zpl, item.Zpl);
	}

	public new virtual PluLabelModel CloneCast() => (PluLabelModel)Clone();

	#endregion
}
