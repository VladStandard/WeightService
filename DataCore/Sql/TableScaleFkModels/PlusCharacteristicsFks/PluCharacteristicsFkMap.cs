// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.PlusCharacteristicsFks;

/// <summary>
/// Table map "PLUS_CHARACTERISTICS_FK".
/// </summary>
public sealed class PluCharacteristicsFkMap : ClassMap<PluCharacteristicsFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluCharacteristicsFkMap()
    {
        Schema(SqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.PlusCharacteristicsFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CREATE_DT").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("CHANGE_DT").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("IS_MARKED").Not.Nullable().Default("0");
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        References(x => x.Characteristic).Column("CHARACTER_UID").Not.Nullable();
    }
}