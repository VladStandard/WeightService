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
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.IsActive).CustomSqlType("BIT").Column("IS_ACTIVE").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        References(x => x.Scale).Column("SCALE_ID").Not.Nullable();
    }
}
