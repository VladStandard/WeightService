// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "CONTRAGENTS_V2".
/// </summary>
public class ContragentV2Entity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Uid;
    public virtual string Name { get; set; }
    public virtual string FullName { get; set; }
    public virtual Guid IdRRef { get; set; }
    public virtual string IdRRefAsString
    {
        get => IdRRef.ToString();
        set => IdRRef = Guid.Parse(value);
    }
    public virtual int DwhId { get; set; }
    public virtual string Xml { get; set; }

    public ContragentV2Entity() : this(Guid.Empty)
    {
        //
    }

    public ContragentV2Entity(Guid uid) : base(uid)
    {
        Name = string.Empty;
        FullName = string.Empty;
        IdRRef = Guid.Empty;
        DwhId = 0;
        Xml = string.Empty;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
	    $"{nameof(IdentityUid)}: {IdentityUid}. " +
		base.ToString() +
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(FullName)}: {FullName}. " +
        $"{nameof(IdRRef)}: {IdRRef}. " +
        $"{nameof(DwhId)}: {DwhId}. " +
        $"{nameof(Xml)}.Length: {Xml?.Length ?? 0}. ";

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
