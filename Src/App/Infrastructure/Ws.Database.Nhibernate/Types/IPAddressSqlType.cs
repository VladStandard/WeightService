// ReSharper disable ClassNeverInstantiated.Global

using System.Data;
using System.Data.Common;
using System.Net;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using Ws.Database.Nhibernate.Common;

namespace Ws.Database.Nhibernate.Types;

internal class IPAddressSqlType : UserType
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.GetString(15)];

    public override Type ReturnedType => typeof(IPAddress);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    {
        object obj = NHibernateUtil.String.NullSafeGet(rs, names, session);
        return obj == null ? null : IPAddress.Parse(obj.ToString() ?? string.Empty);
    }

    public override void NullSafeSet(DbCommand cmd, object? value, int index, ISessionImplementor session)
    {
        ((IDataParameter)cmd.Parameters[index]).Value = value == null ? DBNull.Value : value.ToString();
    }
}