// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLU_REF_V2".
/// </summary>
[Serializable]
public class PluRefV2Entity : BaseEntity, ISerializable
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
    [XmlElement] public virtual PluV2Entity Plu { get; set; } = new();
    [XmlElement] public virtual ScaleEntity Scale { get; set; } = new();

	/// <summary>
	/// Constructor.
	/// </summary>
	public PluRefV2Entity() : this(0)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public PluRefV2Entity(long id) : base(id)
    {
		//
    }

	/// <summary>
	/// Constructor for serialization.
	/// </summary>
	/// <param name="info"></param>
	/// <param name="context"></param>
    protected PluRefV2Entity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
	    Plu = (PluV2Entity)info.GetValue(nameof(Plu), typeof(PluV2Entity));
	    Scale = (ScaleEntity)info.GetValue(nameof(Scale), typeof(ScaleEntity));
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
	    return
		    $"{nameof(IdentityUid)}: {IdentityUid}. " +
		    $"{nameof(IsMarked)}: {IsMarked}. " +
		    $"{nameof(Plu)}: {Plu.Name}. " +
		    $"{nameof(Scale)}: {Scale.Description}. ";
    }

    public virtual bool Equals(PluRefV2Entity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (!Plu.Equals(item.Plu))
            return false;
        if (!Scale.Equals(item.Scale))
            return false;
        return 
	        base.Equals(item) &&
			Equals(Plu, item.Plu) &&
			Equals(Scale, item.Scale);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluRefV2Entity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!Plu.EqualsDefault())
            return false;
        if (!Scale.EqualsDefault())
            return false;
        return 
			base.EqualsDefault();
    }

    public new virtual object Clone()
    {
        PluRefV2Entity item = new();
        item.Plu = Plu.CloneCast();
        item.Scale = Scale.CloneCast();
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PluRefV2Entity CloneCast() => (PluRefV2Entity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(Plu), Plu);
        info.AddValue(nameof(Scale), Scale);
    }
    
    #endregion
}
