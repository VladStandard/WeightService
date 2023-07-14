// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsBlazorCore.Settings;
using WsStorageCore.TableScaleFkModels.DeviceScalesFks;
using WsStorageCore.ViewScaleModels.PluWeightings;
using WsStorageCore.ViewScaleModels.WebLogs;

namespace DeviceControl.Pages.Menu.Operations.PlusWeightings;

public sealed partial class PluWeightings : SectionBase<WsSqlViewPluWeightingModel>
{
    #region Public and private fields, properties, constructor

    private List<WsSqlDeviceScaleFkModel> DeviceScaleFk { get; set; }
    
    private WsSqlViewPluWeightingRepository ViewPluWeightingRepository = WsSqlViewPluWeightingRepository.Instance;
    
    public PluWeightings() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = ViewPluWeightingRepository.GetList(SqlCrudConfigSection);
    }

    #endregion
}