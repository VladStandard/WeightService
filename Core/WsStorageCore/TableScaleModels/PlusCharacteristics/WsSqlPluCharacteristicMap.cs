//This is an independent project of an individual developer. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusCharacteristics;

/// <summary>
/// Table map "NOMENCLATURES_CHARACTERISTICS".
/// </summary>
public sealed class WsSqlPluCharacteristicMap : ClassMap<WsSqlPluCharacteristicModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluCharacteristicMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusCharacteristics);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(128).Not.Nullable().Default("");
        Map(item => item.AttachmentsCount).CustomSqlType("DECIMAL").Column("ATTACHMENTS_COUNT").Not.Nullable();
        Map(item => item.Uid1C).CustomSqlType("UNIQUEIDENTIFIER").Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}