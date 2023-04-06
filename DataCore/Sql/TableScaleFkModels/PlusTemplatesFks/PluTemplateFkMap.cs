// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.PlusTemplatesFks;

/// <summary>
/// Table map "PLUS_TEMPLATES_FK".
/// </summary>
public sealed class PluTemplateFkMap : ClassMap<PluTemplateFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluTemplateFkMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.PlusTemplatesFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        References(x => x.Template).Column("TEMPLATE_ID").Not.Nullable();
    }
}