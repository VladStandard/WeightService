using System.Data.Common;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Ws.Database.Core.Common;

public abstract class UserType : IUserType
{
    #region Abstract

    public abstract SqlType[] SqlTypes { get; }
    public abstract Type ReturnedType { get; }

    public abstract object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner);

    public abstract void NullSafeSet(DbCommand cmd, Object? value, int index, ISessionImplementor session);

    #endregion

    #region Implementation of IUserType

    bool IUserType.Equals(object x, object y) => Equals(x, y);

    public int GetHashCode(Object? x) => x == null ? 0 : x.GetHashCode();

    public virtual object DeepCopy(object value) => value;

    public virtual object Replace(object original, object target, object owner) => original;

    public virtual object Assemble(object cached, object owner) => cached;

    public virtual object Disassemble(object value) => value;

    public bool IsMutable => false;

    #endregion
}