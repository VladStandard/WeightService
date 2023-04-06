// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.Core.Utils;

namespace DataCore.Sql.TableScaleFkModels.PlusStorageMethodsFks;

/// <summary>
/// Table map "PLUS_STORAGE_METHODS_FK".
/// </summary>
public sealed class PluStorageMethodFkMap : ClassMap<PluStorageMethodFkModel>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public PluStorageMethodFkMap()
    {
        Schema(WsSqlSchemaNamesUtils.DbScales);
        Table(WsSqlTableNamesUtils.PlusStorageMethodsFks);
        LazyLoad();
        Id(x => x.IdentityValueUid).CustomSqlType("UNIQUEIDENTIFIER").Column("UID").Unique().GeneratedBy.Guid().Not.Nullable();
        References(x => x.Plu).Column("PLU_UID").Not.Nullable();
        References(x => x.Method).Column("METHOD_UID").Not.Nullable();
        References(x => x.Resource).Column("RESOURCE_UID").Not.Nullable();
    }
}