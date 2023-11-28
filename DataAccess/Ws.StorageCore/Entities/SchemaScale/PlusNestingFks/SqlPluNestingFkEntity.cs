namespace Ws.StorageCore.Entities.SchemaScale.PlusNestingFks;

/// <summary>
/// Доменная модель таблицы PLUS_NESTING_FK.
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class SqlPluNestingFkEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor
    public virtual SqlBoxEntity Box { get; set; }
    public virtual SqlPluEntity Plu { get; set; }
    public virtual bool IsDefault { get; set; }
    public virtual short BundleCount { get; set; }
    public virtual Guid Uid1C { get; set; }
    public override string Name => $"{Plu.Bundle.Name} | {Box.Name}";
    public virtual decimal WeightTare { get => Plu.Bundle.Weight * BundleCount + Box.Weight; set => _ = value; }
    public virtual string WeightTareKg => $"{WeightTare} {LocaleCore.LabelPrint.WeightUnitKg}";
    
    public SqlPluNestingFkEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        Box = new();
        //Plu = new();
        Plu = new();
        IsDefault = false;
        BundleCount = 0;
    }
    
    public SqlPluNestingFkEntity(SqlPluNestingFkEntity item) : base(item)
    {
        Box = new(item.Box);
        Plu = new(item.Plu);
        IsDefault = item.IsDefault;
        BundleCount = item.BundleCount;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsDefault()} | {Plu.Number} | {Plu.Name} | " +
        $"{Plu.Bundle.Weight} * {BundleCount} + {Box.Weight} = {WeightTare}";
        //$" | {PluBundle.Bundle.Name} * {BundleCount} + {Box.Name}";

    protected virtual string GetIsDefault() => IsDefault ? "Is default" : "No default";


    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluNestingFkEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public override void FillProperties()
    {
        base.FillProperties();
        Box.FillProperties();
        Plu.FillProperties();
        BundleCount = 0;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluNestingFkEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Box.Equals(item.Box) &&
        //Plu.Equals(item.Plu) && 
        Plu.Equals(item.Plu) && 
        Equals(IsDefault, item.IsDefault) &&
        Equals(BundleCount, item.BundleCount);
    
    #endregion
}