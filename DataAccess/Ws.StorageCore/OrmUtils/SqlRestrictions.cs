using System;

namespace Ws.StorageCore.OrmUtils;

public static class SqlRestrictions
{
    #region Equal

    public static ICriterion Equal(string propertyName, object value) => Restrictions.Eq(propertyName, value);
    
    public static ICriterion EqualFk(string propertyName, SqlEntityBase item)
    {
        return item.Identity.Name switch
        {
            SqlEnumFieldIdentity.Uid => Restrictions.Eq($"{propertyName}.{nameof(SqlEntityBase.IdentityValueUid)}",
            item.Identity.Uid),
            SqlEnumFieldIdentity.Id => Restrictions.Eq($"{propertyName}.{nameof(SqlEntityBase.IdentityValueId)}",
            item.Identity.Id),
            _ => throw new ArgumentException("Unsupported field identity.")
        };
    }

    public static ICriterion NotEqual(string propertyName, object value) => 
        Restrictions.Not(Restrictions.Eq(propertyName, value));
    
    public static ICriterion EqualUid1C(Guid uid1C) => Restrictions.Eq(nameof(SqlTable1CBase.Uid1C), uid1C);
    
    #endregion

    #region Less

    public static ICriterion Less(string propertyName, object value) => Restrictions.Lt(propertyName, value);
    
    public static ICriterion LessOrEqual(string propertyName, object value) => Restrictions.Le(propertyName, value);
    
    #endregion

    #region More
    
    public static ICriterion More(string propertyName, object value) => Restrictions.Gt(propertyName, value);
    
    public static ICriterion MoreOrEqual(string propertyName, object value) => Restrictions.Ge(propertyName, value);
    
    #endregion

    #region In

    public static ICriterion In<T>(string propertyName, List<T> value) => Restrictions.In(propertyName, value);
    
    public static ICriterion NotIn(string propertyName, List<object> value) => 
        Restrictions.Not(Restrictions.In(propertyName, value));

    #endregion
}