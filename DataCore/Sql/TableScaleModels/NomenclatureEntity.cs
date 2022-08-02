// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Models;

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table "Nomenclature".
/// </summary>
public class NomenclatureEntity : BaseEntity
{
    #region Public and private fields, properties, constructor

    /// <summary>
    /// Identity name.
    /// </summary>
    public static ColumnName IdentityName => ColumnName.Id;
    public virtual string Name { get; set; }
    public virtual string Code { get; set; }
    public virtual string SerializedRepresentationObject { get; set; }
    /// <summary>
    /// Is weighted or pcs.
    /// </summary>
    public virtual bool Weighted { get; set; }

    public NomenclatureEntity() : this(0)
    {
        //
    }

    public NomenclatureEntity(long id) : base(id)
    {
        Name = string.Empty;
        Code = string.Empty;
        SerializedRepresentationObject = string.Empty;
        Weighted = false;
    }

    #endregion

    #region Public and private methods

    public override string ToString() =>
        base.ToString() +
        $"{nameof(Code)}: {Code}. " +
        $"{nameof(SerializedRepresentationObject)}.Length: {SerializedRepresentationObject?.Length ?? 0}. " +
        $"{nameof(Weighted)}: {Weighted}. ";

    public virtual bool Equals(NomenclatureEntity item)
    {
        if (item is null) return false;
        if (ReferenceEquals(this, item)) return true;
        return base.Equals(item) &&
               Equals(Code, item.Code) &&
               Equals(Name, item.Name) &&
               Equals(SerializedRepresentationObject, item.SerializedRepresentationObject) &&
               Equals(Weighted, item.Weighted);
    }

    public override bool Equals(object obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((NomenclatureEntity)obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public virtual bool EqualsNew()
    {
        return Equals(new());
    }

    public new virtual bool EqualsDefault()
    {
        return base.EqualsDefault() &&
               Equals(Code, string.Empty) &&
               Equals(Name, string.Empty) &&
               Equals(SerializedRepresentationObject, string.Empty) &&
               Equals(Weighted, false);
    }

    public new virtual object Clone()
    {
        NomenclatureEntity item = new();
        item.Code = Code;
        item.Name = Name;
        item.SerializedRepresentationObject = SerializedRepresentationObject;
        item.Weighted = Weighted;
        item.Setup(((BaseEntity)this).CloneCast());
        return item;
    }

    public new virtual NomenclatureEntity CloneCast() => (NomenclatureEntity)Clone();

    #endregion
}
