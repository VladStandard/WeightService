// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.TableScaleModels.Bundles;

/// <summary>
/// Table map "BUNDLES".
/// </summary>
public sealed class BundleMap : ClassMap<BundleModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public BundleMap()
    {
        Schema(WsSqlSchemasUtils.DbScales);
        Table(WsSqlTablesUtils.Bundles);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Unique().Length(128).Not.Nullable().Default("");
        Map(x => x.Weight).CustomSqlType("DECIMAL(10,3)").Column("WEIGHT").Not.Nullable();
        Map(x => x.Uid1c).CustomSqlType("UNIQUEIDENTIFIER").Column("UID_1C").Not.Nullable().Default(Guid.Empty.ToString());
    }
}