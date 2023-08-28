namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class BarCodes : SectionBase<WsSqlViewBarcodeModel>
{
    #region Public and private fields, properties, constructor

    private WsSqlViewBarcodeRepository ViewBarcodeRepository { get; } = new();
    
    public BarCodes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast =  ViewBarcodeRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
