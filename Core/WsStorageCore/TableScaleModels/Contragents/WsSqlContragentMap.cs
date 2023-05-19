// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Contragents;

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
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("MARKED").Not.Nullable().Default("0");
        Map(item => item.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(200).Not.Nullable();
        Map(item => item.FullName).CustomSqlType("NVARCHAR(MAX)").Column("FULL_NAME").Not.Nullable();
        Map(item => item.DwhId).CustomSqlType("INT").Column("DWH_ID").Not.Nullable();
        Map(item => item.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IDRREF").Nullable();
        Map(item => item.Xml).CustomSqlType("XML").Column("XML").Nullable();
    }
}
