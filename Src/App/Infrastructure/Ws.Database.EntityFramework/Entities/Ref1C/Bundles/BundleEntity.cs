namespace Ws.Database.EntityFramework.Entities.Ref1C.Bundles;

[Table(SqlTables.Bundles, Schema = SqlSchemas.Ref1C)]
public sealed class BundleEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [RegularExpression(@"^\d+\.\d{3}$", ErrorMessage = "Weight must have exactly three decimal places")]
    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public BundleEntity() { }

    public BundleEntity(Guid uid, string name, decimal weight, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        Weight = weight;
        ChangeDt = updateDate;
    }
}