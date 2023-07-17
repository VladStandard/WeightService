// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableScaleModels.Contragents;

/// <summary>
/// Table map "CONTRAGENTS_V2".
/// </summary>
public sealed class WsSqlContragentMap : ClassMap<WsSqlContragentModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlContragentMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Contragents);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("NAME").Length(200).Not.Nullable();
        Map(item => item.FullName).CustomSqlType(WsSqlFieldTypeUtils.NvarChar).Column("FULL_NAME").Not.Nullable();
        Map(item => item.DwhId).CustomSqlType(WsSqlFieldTypeUtils.Int).Column("DWH_ID").Not.Nullable();
        Map(item => item.IdRRef).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("IDRREF").Nullable();
        Map(item => item.Xml).CustomSqlType(WsSqlFieldTypeUtils.Xml).Column("XML").Nullable();
    }
}
