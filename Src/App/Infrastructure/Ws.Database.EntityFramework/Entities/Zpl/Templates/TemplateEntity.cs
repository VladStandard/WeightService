namespace Ws.Database.EntityFramework.Entities.Zpl.Templates;

[Table(SqlTables.Templates, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), nameof(IsWeight), Name = $"UQ_{SqlTables.Templates}_NAME_IS_WEIGHT", IsUnique = true)]
public sealed class TemplateEntity : EfEntityBase
{
    [Column(SqlColumns.Name), StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Column("BODY")]
    public string Body { get; set; } = string.Empty;

    [Column("IS_WEIGHT")]
    public bool IsWeight { get; set; }

    [Column("WIDTH")]
    public short Width { get; set; }

    [Column("HEIGHT")]
    public short Height { get; set; }

    [Column("BARCODE_TOP_BODY"), StringLength(2048)]
    public string BarcodeTopBody { get; set; } = string.Empty;

    [Column("BARCODE_BOTTOM_BODY"), StringLength(2048)]
    public string BarcodeBottomBody { get; set; } = string.Empty;

    [Column("BARCODE_RIGHT_BODY"), StringLength(2048)]
    public string BarcodeRightBody { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}