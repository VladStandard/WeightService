// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "ACCESS".
/// </summary>
public class AccessMap : ClassMap<AccessModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public AccessMap()
    {
        Schema("db_scales");
        Table("ACCESS");
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.User).CustomSqlType("NVARCHAR").Column("USER").Length(32).Not.Nullable();
        Map(x => x.Rights).CustomSqlType("TINYINT").Column("RIGHTS").Not.Nullable().Default("0");
    }
}
