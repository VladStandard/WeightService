// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "LOG_TYPES".
/// </summary>
public class LogTypeMap : ClassMap<LogTypeEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogTypeMap()
    {
        Schema("db_scales");
        Table("LOG_TYPES");
        LazyLoad();
        Id(x => x.IdentityUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Number).CustomSqlType("TINYINT").Column("NUMBER").Not.Nullable();
        Map(x => x.Icon).CustomSqlType("NVARCHAR").Column("ICON").Length(32).Not.Nullable();
    }
}
