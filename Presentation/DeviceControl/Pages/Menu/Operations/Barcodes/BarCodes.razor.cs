namespace DeviceControl.Pages.Menu.Operations.Barcodes;

public sealed partial class BarCodes : SectionBase<SqlViewBarcodeModel>
{
    #region Public and private fields, properties, constructor

    private SqlViewBarcodeRepository ViewBarcodeRepository { get; } = new();
    
    public BarCodes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast =  ViewBarcodeRepository.GetList(SqlCrudConfigSection).ToList();
    }

    #endregion
}
