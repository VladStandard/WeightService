namespace Ws.Database.EntityFramework.Entities.Zpl.Templates;

[Table(SqlTables.Templates, Schema = SqlSchemas.Zpl)]
[Index(nameof(Name), nameof(IsWeight), Name = $"UQ_{SqlTables.Templates}_NAME_IS_WEIGHT", IsUnique = true)]
public sealed class TemplateEntity : EfEntityBase
{
    [Column(SqlColumns.Name)]
    [StringLength(64)]
    public string Name { get; set; } = string.Empty;

    [Column("BODY")]
    [StringLength(10240)]
    public string Body { get; set; } = string.Empty;

    [Column("IS_WEIGHT")]
    public bool IsWeight { get; set; }

    [Column("WIDTH")]
    public short Width { get; set; }

    [Column("HEIGHT")]
    public short Height { get; set; }

    [StringLength(256)]
    [Column("BARCODE_TOP_BODY")]
    public string BarcodeTopBody { get; set; }

    [StringLength(256)]
    [Column("BARCODE_BOTTOM_BODY")]
    public string BarcodeBottomBody { get; set; }

    [StringLength(256)]
    [Column("BARCODE_RIGHT_BODY")]
    public string BarcodeRightBody { get; set; }

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}