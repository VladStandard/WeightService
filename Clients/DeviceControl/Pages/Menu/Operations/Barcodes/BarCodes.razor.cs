// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

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
        SqlSectionCast =  ViewBarcodeRepository.GetList(SqlCrudConfigSection);
    }

    #endregion
}