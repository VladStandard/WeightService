namespace Ws.Database.EntityFramework.Entities.Zpl.Templates;

public sealed class TemplateEntity : EfEntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public bool IsWeight { get; set; }

    public short Width { get; set; }
    public short Height { get; set; }
    public short Rotate { get; set; }

    public string BarcodeTopBody { get; set; } = string.Empty;
    public string BarcodeBottomBody { get; set; } = string.Empty;
    public string BarcodeRightBody { get; set; } = string.Empty;

    #region Date

    public DateTime CreateDt { get; init; }
    public DateTime ChangeDt { get; init; }

    #endregion
}