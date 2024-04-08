namespace Ws.Database.EntityFramework.Entities.Zpl.Templates;

[Table(SqlTables.Templates, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), Name = $"UQ_{SqlTables.Templates}_NAME", IsUnique = true)]
public sealed class TemplateEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Column("BODY")]
    [StringLength(10240)]
    public string Body { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
    // public virtual ICollection<PlusTemplatesFk> PlusTemplatesFks { get; set; } = new List<PlusTemplatesFk>();
}