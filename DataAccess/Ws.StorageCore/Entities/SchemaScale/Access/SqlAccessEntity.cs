using System;
using Ws.Shared.Enums;

namespace Ws.StorageCore.Entities.SchemaScale.Access;

[DebuggerDisplay("{ToString()}")]
public class SqlAccessEntity : SqlEntityBase
{
    #region Public and private fields, properties, constructor

    public virtual DateTime LoginDt { get; set; }
    public virtual byte Rights { get; set; }
    public virtual EnumAccessRights RightsEnum => (EnumAccessRights)Rights;
    
    public SqlAccessEntity() : base(SqlEnumFieldIdentity.Uid)
    {
        LoginDt = DateTime.MinValue;
        Rights = 0x00;
    }

    public SqlAccessEntity(SqlAccessEntity item) : base(item)
    {
        LoginDt = item.LoginDt;
        Rights = item.Rights;
    }

    #endregion

    #region Public and private methods - override

    public override string ToString() => $"{Name} | {RightsEnum}";

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SqlAccessEntity)obj);
    }

    public override int GetHashCode() => base.GetHashCode();
    
    public override void FillProperties()
    {
        base.FillProperties();
        LoginDt = DateTime.Now;
        Name = "KOLBASA-VS\\";
        Rights = (byte)EnumAccessRights.None;
    }

    #endregion

    #region Public and private methods - virtual

    public virtual bool Equals(SqlAccessEntity item) =>
        ReferenceEquals(this, item) || base.Equals(item) && //-V3130
        Equals(LoginDt, item.LoginDt) &&
        Equals(Rights, item.Rights);

    #endregion
}
