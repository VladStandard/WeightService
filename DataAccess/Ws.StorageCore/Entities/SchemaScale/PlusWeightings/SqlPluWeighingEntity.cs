// ReSharper disable VirtualMemberCallInConstructor

using Ws.StorageCore.Common;
using Ws.StorageCore.Entities.SchemaScale.PlusScales;
namespace Ws.StorageCore.Entities.SchemaScale.PlusWeightings;

[DebuggerDisplay("{ToString()}")]
public class SqlPluWeighingEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual SqlPluScaleEntity PluScale { get; set; }
    public virtual short Kneading { get; set; }
    public virtual decimal NettoWeight { get; set; }
    public virtual decimal WeightTare { get; set; }
    
    public SqlPluWeighingEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        PluScale = new();
        Kneading = 0;
        NettoWeight = 0;
        WeightTare = 0;
    }

    public SqlPluWeighingEntity(SqlPluWeighingEntity item) : base(item)
    {
        PluScale = new(item.PluScale);
        Kneading = item.Kneading;
        NettoWeight = item.NettoWeight;
        WeightTare = item.WeightTare;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() =>
        $"{GetIsMarked()} | " +
        $"{nameof(Kneading)}: {Kneading}. " +
        $"{nameof(PluScale)}: {PluScale}. ";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlPluWeighingEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();

    public override bool EqualsNew() => Equals(new());

    public override bool EqualsDefault() =>
        base.EqualsDefault() &&
        Equals(Kneading, default(short)) &&
        Equals(NettoWeight, default(decimal)) &&
        Equals(WeightTare, default(decimal)) &&
        PluScale.EqualsDefault();

    public override void FillProperties()
    {
        base.FillProperties();
        NettoWeight = 1.1M;
        WeightTare = 0.25M;
        Kneading = 1;
        PluScale.FillProperties();
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlPluWeighingEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) &&
        Equals(Kneading, item.Kneading) &&
        Equals(PluScale, item.PluScale) &&
        Equals(NettoWeight, item.NettoWeight) &&
        Equals(WeightTare, item.WeightTare) &&
        PluScale.Equals(item.PluScale);

    #endregion
}
