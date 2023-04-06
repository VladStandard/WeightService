// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleModels.Contragents;

/// <summary>
/// Table map "CONTRAGENTS_V2".
/// </summary>
public sealed class ContragentMap : ClassMap<ContragentModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public ContragentMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.Contragents);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("MARKED").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("NAME").Length(200).Not.Nullable();
        Map(x => x.FullName).CustomSqlType("NVARCHAR(MAX)").Column("FULL_NAME").Not.Nullable();
        Map(x => x.DwhId).CustomSqlType("INT").Column("DWH_ID").Not.Nullable();
        Map(x => x.IdRRef).CustomSqlType("UNIQUEIDENTIFIER").Column("IDRREF").Nullable();
        Map(x => x.Xml).CustomSqlType("XML").Column("XML").Nullable();
    }
}
