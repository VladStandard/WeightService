﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels.LogsTypes;

/// <summary>
/// Table map "LOG_TYPES".
/// </summary>
public class LogTypeMap : ClassMap<LogTypeModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogTypeMap()
    {
        Schema("db_scales");
        Table(SqlTableNamesUtils.LogsTypes);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Number).CustomSqlType("TINYINT").Column("NUMBER").Not.Nullable();
        Map(x => x.Icon).CustomSqlType("NVARCHAR").Column("ICON").Length(32).Not.Nullable();
    }
}
