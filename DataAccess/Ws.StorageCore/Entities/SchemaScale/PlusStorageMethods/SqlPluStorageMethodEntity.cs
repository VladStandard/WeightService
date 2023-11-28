// ReSharper disable VirtualMemberCallInConstructor

namespace Ws.StorageCore.Entities.SchemaScale.PlusStorageMethods;

[Serializable]
[DebuggerDisplay("{ToString()}")]
public class SqlPluStorageMethodEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual short MinTemp { get; set; }
    public virtual short MaxTemp { get; set; }
    
    public SqlPluStorageMethodEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        MinTemp = default;
        MaxTemp = default;
    }

    public SqlPluStorageMethodEntity(SqlPluStorageMethodEntity item) : base(item)
    {
        MinTemp = item.MinTemp;
        MaxTemp = item.MaxTemp;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{nameof(Name)}: {Name}. " +
        $"{nameof(MinTemp)}: {MinTemp}. " +
        $"{nameof(MaxTemp)}: {MaxTemp}.";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluStorageMethodEntity)obj);
    }

    public override int GetHashCode() => IdentityValueUid.GetHashCode();
    
    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluStorageMethodEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(MinTemp, item.MinTemp) &&
        Equals(MaxTemp, item.MaxTemp);

    #endregion
}