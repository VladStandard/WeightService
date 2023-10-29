using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using WsStorageCore.OrmUtils;
namespace WsStorageCore.Entities.SchemaConf.DeviceSettingsFks;

/// <summary>
/// Маппинг таблицы "DEVICES_SETTINGS_FK".
/// </summary>
public sealed class WsSqlDeviceSettingsFkMap : ClassMapping<WsSqlDeviceSettingsFkEntity>
{
    public WsSqlDeviceSettingsFkMap()
    {
        Schema(WsSqlSchemasUtils.Conf);
        Table(WsSqlTablesUtils.DeviceSettingsFks);

        Id(x => x.IdentityValueUid, m => {
            m.Column("UID");
            m.Type(NHibernateUtil.Guid);
            m.Generator(Generators.Guid);
        });

        Property(x => x.IsEnabled, m => {
            m.Column("IS_ENABLED");
            m.Type(NHibernateUtil.Boolean);
            m.NotNullable(true);
        });

        ManyToOne(x => x.Host, m => {
            m.Column("DEVICE_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });

        ManyToOne(x => x.Setting, m => {
            m.Column("SETTING_UID");
            m.NotNullable(true);
            m.Lazy(LazyRelation.NoLazy);
        });
    }
}