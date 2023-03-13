// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.PlusStorageMethods;

/// <summary>
/// Table map "PLUS_STORAGE_METHODS".
/// </summary>
public class PlusStorageMethodMap : ClassMap<PlusStorageMethodModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PlusStorageMethodMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(SqlTableNamesUtils.PlusStorageMethods);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        Map(x => x.MinTemp).CustomSqlType("SMALLINT").Column("MIN_TEMP").Not.Nullable().Default("0");
        Map(x => x.MaxTemp).CustomSqlType("SMALLINT").Column("MAX_TEMP").Not.Nullable().Default("0");
    }
}