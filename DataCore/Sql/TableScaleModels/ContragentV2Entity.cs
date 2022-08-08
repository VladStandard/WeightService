// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "CONTRAGENTS_V2".
/// </summary>
[Serializable]
public class ContragentV2Entity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    [XmlElement] public static ColumnName IdentityName => ColumnName.Uid;
    [XmlElement] public virtual string Name { get; set; } = string.Empty;
    [XmlElement] public virtual string FullName { get; set; } = string.Empty;
    [XmlElement] public virtual Guid IdRRef { get; set; } = Guid.Empty;
    public virtual string IdRRefAsString
    {
        get => IdRRef.ToString();
        set => IdRRef = Guid.Parse(value);
    }
    [XmlElement] public virtual int DwhId { get; set; }
    [XmlElement] public virtual string Xml { get; set; } = string.Empty;

	/// <summary>
	/// Constructor.
	/// </summary>
    public ContragentV2Entity() : this(Guid.Empty)
    {
        //
    }

	/// <summary>
	/// Constructor.
	/// </summary>
    public ContragentV2Entity(Guid uid) : base(uid)
    {
        //
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        $"{nameof(IdentityUid)}: {IdentityUid}. " +
        $"{nameof(IsMarked)}: {IsMarked}. " +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(DwhId)}: {DwhId}. ";

    public virtual bool Equals(ContragentV2Entity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Name, item.Name) &&
               Equals(FullName, item.FullName) &&
               Equals(IdRRef, item.IdRRef) &&
               Equals(DwhId, item.DwhId) &&
               Equals(Xml, item.Xml);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ContragentV2Entity)obj);
    }

	public override int GetHashCode() => IdentityUid.GetHashCode();

	public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Name, string.Empty) &&
               Equals(FullName, string.Empty) &&
               Equals(IdRRef, Guid.Empty) &&
               Equals(DwhId, 0) &&
               Equals(Xml, string.Empty);
    }

    public new virtual object Clone()
    {
        ContragentV2Entity item = new();
        item.Name = Name;
        item.FullName = FullName;
        item.IdRRef = IdRRef;
        item.DwhId = DwhId;
        item.Xml = Xml;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual ContragentV2Entity CloneCast() => (ContragentV2Entity)Clone();

    #endregion
}
