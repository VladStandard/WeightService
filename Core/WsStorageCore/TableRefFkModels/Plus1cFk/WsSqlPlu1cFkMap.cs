// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableRefFkModels.Plus1CFk;

/// <summary>
/// Маппинг полей таблицы REF.PLUS_1C_FK.
/// </summary>
public sealed class WsSqlPlu1CFkMap : ClassMap<WsSqlPlu1CFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPlu1CFkMap()
    {
        Schema(WsSqlSchemasUtils.Ref);
        Table(WsSqlTablesUtils.Plus1cFk);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        Map(x => x.IsEnabled).CustomSqlType("BIT").Column("IS_ENABLED").Not.Nullable().Default("0");
        Map(x => x.RequestDataString).CustomSqlType("NVARCHAR").Column("REQUEST_DATA_STRING").Not.Nullable().Default("");
    }
}