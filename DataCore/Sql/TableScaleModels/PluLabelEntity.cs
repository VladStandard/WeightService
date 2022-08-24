// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
// ReSharper disable VirtualMemberCallInConstructor

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "PLUS_LABELS".
/// </summary>
[Serializable]
public class PluLabelEntity : BaseEntity, ISerializable, IBaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    [XmlElement] public static ColumnName IdentityName => ColumnName.Uid;

    [XmlElement] public virtual PluWeighingEntity? PluWeighing { get; set; }
    [XmlElement] public virtual string Zpl { get; set; }

    /// <summary>
    /// Constructor.
    /// </summary>
    public PluLabelEntity() : base(Guid.Empty, false)
    {
        Init();
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="identityUid"></param>
    /// <param name="isSetupDates"></param>
    public PluLabelEntity(Guid identityUid, bool isSetupDates) : base(identityUid, isSetupDates)
	{
        Init();
    }

    /// <summary>
    /// Constructor for serialization.
    /// </summary>
    /// <param name="info"></param>
    /// <param name="context"></param>
    protected PluLabelEntity(SerializationInfo info, StreamingContext context) : base(info, context)
    {
        PluWeighing = (PluWeighingEntity)info.GetValue(nameof(PluWeighing), typeof(PluWeighingEntity));
        Zpl = info.GetString(nameof(Zpl));
    }

    #endregion

    #region Public and private methods

    public new virtual void Init()
    {
        base.Init();
        PluWeighing = null;
        Zpl = string.Empty;
    }

    public override string ToString()
    {
	    string strPluWeighing = PluWeighing == null ? string.Empty : PluWeighing.ToString();
		return
            $"{nameof(IdentityUid)}: {IdentityUid}. " +
            $"{nameof(PluWeighing)}: {strPluWeighing}. ";
    }

    public virtual bool Equals(PluLabelEntity item)
    {
        //if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (PluWeighing != null && item.PluWeighing != null && !PluWeighing.Equals(item.PluWeighing))
            return false;
        return
            base.Equals(item) &&
            Equals(PluWeighing, item.PluWeighing) &&
            Equals(Zpl, item.Zpl);
    }

    public override bool Equals(object obj)
    {
        //if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PluLabelEntity)obj);
    }

    public override int GetHashCode() => IdentityUid.GetHashCode();

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (PluWeighing != null && !PluWeighing.EqualsDefault())
            return false;
        return
            base.EqualsDefault() &&
            Equals(Zpl, string.Empty);
    }

    public new virtual object Clone()
    {
        PluLabelEntity item = new();
        item.IsMarked = IsMarked;
        item.PluWeighing = PluWeighing?.CloneCast();
        item.Zpl = Zpl;
		item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual PluLabelEntity CloneCast() => (PluLabelEntity)Clone();

    public new virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
        info.AddValue(nameof(PluWeighing), PluWeighing);
        info.AddValue(nameof(Zpl), Zpl);
    }

    #endregion
}
