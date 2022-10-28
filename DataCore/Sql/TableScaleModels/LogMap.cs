// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "LOGS".
/// </summary>
public class LogMap : ClassMap<LogModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public LogMap()
    {
        Schema("db_scales");
        Table("LOGS");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.Version).CustomSqlType("NVARCHAR").Column("VERSION").Length(12).Nullable();
        Map(x => x.File).CustomSqlType("NVARCHAR").Column("FILE").Length(40).Not.Nullable();
        Map(x => x.Line).CustomSqlType("INT").Column("LINE").Not.Nullable();
        Map(x => x.Member).CustomSqlType("NVARCHAR").Column("MEMBER").Length(40).Not.Nullable();
        Map(x => x.Message).CustomSqlType("NVARCHAR").Column("MESSAGE").Length(1024).Not.Nullable();
        //References(x => x.Host).Column("HOST_ID").Nullable();
        References(x => x.Device).Column("DEVICE_UID").Nullable();
        References(x => x.App).Column("APP_UID").Nullable();
        References(x => x.LogType).Column("LOG_TYPE_UID").Nullable();
    }
}
