using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;

namespace WsStorageCore.Tables.TableConfModels.DeviceSettings;

/// <summary>
/// Маппинг таблицы "DEVICES_SETTINGS".
/// </summary>
public sealed class WsSqlDeviceSettingsMap : ClassMapping<WsSqlDeviceSettingsModel>
{
    public WsSqlDeviceSettingsMap()
    {
        Schema(WsSqlSchemasUtils.Conf);
        Table(WsSqlTablesUtils.DeviceSettings);

        Id(x => x.IdentityValueUid, m =>
        {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.CreateDt, m =>
        {
            m.Column("DT_CREATE");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.ChangeDt, m =>
        {
            m.Column("DT_CHANGE");
            m.Type(NHibernateUtil.DateTime);
            m.NotNullable(true);
        });

        Property(x => x.IsMarked, m =>
        {
            m.Column("IS_MARKED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        Property(x => x.Name, m =>
        {
            m.Column("NAME");
            m.Type(NHibernateUtil.String);
            m.Length(128);
            m.NotNullable(true);
        });
    }
}