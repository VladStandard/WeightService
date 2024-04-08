namespace Ws.Database.EntityFramework.Entities.Ref1C.Clips;

[Table(SqlTables.Clips, Schema = SqlSchemas.Ref1C)]
public sealed class ClipEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Range(0, 1.000, ErrorMessage = "Weight must be less than 1.000")]
    [RegularExpression(@"^\d+\.\d{3}$")]
    [Column(SqlColumns.Weight, TypeName = "decimal(4,3)")]
    public decimal Weight { get; set; }

    [NotMapped] public override bool IsNew => CreateDt.Equals(DateTime.MinValue);

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion

    public ClipEntity() { }

    public ClipEntity(Guid uid, string name, decimal weight, DateTime updateDate)
    {
        Id = uid;
        Name = name;
        Weight = weight;
        ChangeDt = updateDate;
    }
}