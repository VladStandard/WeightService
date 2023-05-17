// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsStorageCore.Models;

/// <summary>
/// SQL table map "EMPTY".
/// </summary>
[DebuggerDisplay("{ToString()}")]
public class WsSqlTableEmptyMap : ClassMap<WsSqlTableEmptyModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public WsSqlTableEmptyMap()
    {
        //Schema(SqlSchemaNamesUtils.DbScales);
        //Table("EMPTY");
        LazyLoad();
        Id(item => item.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(item => item.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(item => item.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(item => item.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
    }
}