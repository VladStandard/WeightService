// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.LogsWebs;

/// <summary>
/// Table map "LOGS_WEBS".
/// </summary>
public class LogWebMap : ClassMap<LogWebModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogWebMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.Logs);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.StampDt).CustomSqlType("DATETIME").Column("STAMP_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Version).CustomSqlType("NVARCHAR").Column("VERSION").Length(12).Nullable();
        Map(x => x.File).CustomSqlType("NVARCHAR").Column("FILE").Length(64).Not.Nullable();
        Map(x => x.Line).CustomSqlType("INT").Column("LINE").Not.Nullable();
        Map(x => x.Member).CustomSqlType("NVARCHAR").Column("MEMBER").Length(64).Not.Nullable();
        Map(x => x.Direction).CustomSqlType("BIT").Column("DIRECTION").Nullable();
        Map(x => x.Url).CustomSqlType("NVARCHAR").Column("URL").Length(512).Not.Nullable();
        Map(x => x.Params).CustomSqlType("NVARCHAR").Column("PARAMS").Length(1024).Nullable();
        Map(x => x.Headers).CustomSqlType("NVARCHAR").Column("HEADERS").Length(1024).Nullable();
        Map(x => x.Xml).CustomSqlType("XML").Column("XML").Nullable();
        Map(x => x.CountAll).CustomSqlType("INT").Column("COUNT_ALL").Not.Nullable();
        Map(x => x.CountSuccess).CustomSqlType("INT").Column("COUNT_SUCCESS").Not.Nullable();
        Map(x => x.CountErrors).CustomSqlType("INT").Column("COUNT_ERROR").Not.Nullable();
    }
}