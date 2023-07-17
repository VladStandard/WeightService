// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Tables.TableDiagModels.ScalesScreenshots;

/// <summary>
/// Table map "diag.SCALES_SCREENSHOTS".
/// </summary>
public sealed class WsSqlScaleScreenShotMap : ClassMap<WsSqlScaleScreenShotModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlScaleScreenShotMap()
    {
        Schema(WsSqlSchemasUtils.Diag);
        Table(WsSqlTablesUtils.ScalesScreenshots);
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType(WsSqlFieldTypeUtils.UniqueIdentifier).Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType(WsSqlFieldTypeUtils.DateTime).Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType(WsSqlFieldTypeUtils.Bit).Column("IS_MARKED").Not.Nullable().Default("0");
        Map(item => item.ScreenShot).CustomSqlType(WsSqlFieldTypeUtils.VarBinary).Length(int.MaxValue).Column("SCREENSHOT").Not.Nullable().Default($"{0x00}");
        References(item => item.Scale).Column("SCALE_ID").Not.Nullable();
    }
}