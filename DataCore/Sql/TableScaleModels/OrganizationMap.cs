// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCore.Sql.TableScaleModels;

/// <summary>
/// Table map "Organization".
/// </summary>
public class OrganizationMap : ClassMap<OrganizationEntity>
{
    /// <summary>
    /// Constructor.
    /// </summary>
    public OrganizationMap()
    {
        Schema("db_scales");
        Table("Organization");
        LazyLoad();
        Id(x => x.IdentityId).CustomSqlType("INT").Column("Id").Unique().GeneratedBy.Identity().Not.Nullable();
        Map(x => x.CreateDt).CustomSqlType("DATETIME").Column("CreateDate").Not.Nullable();
        Map(x => x.ChangeDt).CustomSqlType("DATETIME").Column("ModifiedDate").Not.Nullable();
        Map(x => x.IsMarked).CustomSqlType("BIT").Column("Marked").Not.Nullable().Default("0");
        Map(x => x.Name).CustomSqlType("NVARCHAR").Column("CategoryID").Length(150).Not.Nullable();
        Map(x => x.Gln).CustomSqlType("INT").Column("GLN").Not.Nullable();
        Map(x => x.SerializedRepresentationObject).CustomSqlType("XML").Column("SerializedRepresentationObject").Nullable();
    }
}
