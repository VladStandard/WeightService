// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Labels".
/// </summary>
[Serializable]
public class LabelEntity : BaseEntity
{
	#region Public and private fields, properties, constructor

	/// <summary>
	/// Identity name.
	/// </summary>
	[XmlElement] public static ColumnName IdentityName => ColumnName.Id;
    [XmlElement] public virtual WeithingFactEntity WeithingFact { get; set; } = new();
    [XmlElement(IsNullable = true)] public virtual byte[]? Label { get; set; } = Array.Empty<byte>();
	[XmlElement] public virtual string LabelString
    {
        get => Label == null || Label.Length == 0 ? string.Empty : Encoding.Default.GetString(Label);
        set => Label = Encoding.Default.GetBytes(value);
    }
	[XmlElement] public virtual string LabelInfo
    {
        get => DataUtils.GetBytesLength(Label);
        set => _ = value;
    }
	[XmlElement] public virtual string Zpl { get; set; } = string.Empty;
	[XmlElement] public virtual string ZplInfo
    {
        get => DataUtils.GetStringLength(Zpl);
        set => _ = value;
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public LabelEntity() : this(0)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public LabelEntity(long id) : base(id)
    {
        // 
    }

    #endregion

    #region Public and private methods

    public override string ToString()
    {
        return
			$"{nameof(IdentityId)}: {IdentityId}. " + 
			$"{nameof(IsMarked)}: {IsMarked}. " +
            $"{nameof(WeithingFact)}: {WeithingFact.IdentityId}. " +
			$"{nameof(Zpl)}: {ZplInfo}. ";
    }

    public virtual bool Equals(LabelEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        if (!WeithingFact.Equals(item.WeithingFact))
            return false;
        return base.Equals(item) &&
               DataUtils.ByteEquals(Label, item.Label) &&
               Equals(Zpl, item.Zpl);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((LabelEntity)obj);
    }

	public override int GetHashCode() => IdentityId.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        if (!WeithingFact.EqualsDefault())
            return false;
        return base.EqualsDefault() &&
               DataUtils.ByteEquals(Label, new byte[0]) &&
               Equals(Zpl, string.Empty);
    }

    public new virtual object Clone()
    {
        LabelEntity item = new();
        item.WeithingFact = WeithingFact.CloneCast();
        item.Label = Label == null ? null : DataUtils.ByteClone(Label);
        item.Zpl = Zpl;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual LabelEntity CloneCast() => (LabelEntity)Clone();

    #endregion
}
