using System.Data;
using System.Data.Common;
using Newtonsoft.Json;
using NHibernate.Engine;
using NHibernate.SqlTypes;
using Ws.Database.Nhibernate.Common;
using Ws.Domain.Models.ValueTypes;

namespace Ws.Database.Nhibernate.Types;

internal class BarcodeItemSqlType : UserType
{
    public override SqlType[] SqlTypes => [SqlTypeFactory.GetString(256)];

    public override Type ReturnedType => typeof(Dictionary<string, BarcodeItem>);

    public override object? NullSafeGet(DbDataReader rs, string[] names, ISessionImplementor session, object owner)
    {
        object obj = NHibernateUtil.String.NullSafeGet(rs, names, session);
        return obj == null || obj.ToString() == string.Empty ? new Dictionary<string, BarcodeItem>() : JsonConvert.DeserializeObject<Dictionary<string, BarcodeItem>>(obj.ToString());
    }

    public override void NullSafeSet(DbCommand cmd, object? value, int index, ISessionImplementor session)
    {
        ((IDataParameter)cmd.Parameters[index]).Value = value == null ? DBNull.Value : JsonConvert.SerializeObject(value);
    }
}