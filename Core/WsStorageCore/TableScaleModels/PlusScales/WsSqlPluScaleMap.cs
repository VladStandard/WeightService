// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.PlusScales;

/// <summary>
/// Table map "PLUS_SCALES".
/// </summary>
public sealed class WsSqlPluScaleMap : ClassMap<WsSqlPluScaleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlPluScaleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.PlusScales);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.IsActive).CustomSqlType("BIT").Column("IS_ACTIVE").Not.Nullable().Default("0");
        References(item => item.Plu).Column("PLU_UID").Not.Nullable();
        References(item => item.Scale).Column("SCALE_ID").Not.Nullable();
    }
}
