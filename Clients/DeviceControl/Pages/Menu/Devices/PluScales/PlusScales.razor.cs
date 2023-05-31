// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.TableScaleFkModels.PlusBundlesFks;
using WsStorageCore.TableScaleModels.Plus;
using WsStorageCore.TableScaleModels.PlusScales;
using WsStorageCore.TableScaleModels.Scales;

namespace DeviceControl.Pages.Menu.Devices.PluScales;

public sealed partial class PlusScales : RazorComponentSectionBase<WsSqlPluScaleModel>
{
    #region Public and private fields, properties, constructor

    [Parameter] public WsSqlScaleModel Scale { get; set; }
    public bool HideNoneActivePlu { get; set; }

    public PlusScales() : base()
    {
        HideNoneActivePlu = true;
        SqlCrudConfigSection.IsGuiShowFilterAdditional = true;
        SqlCrudConfigSection.IsResultOrder = true;
        ButtonSettings = new(true, true, true, true, true, true, false);
    }

    #endregion

    #region Public and private methods

    protected override void SetSqlSectionCast()
    {
        if (!HideNoneActivePlu) 
            SqlCrudConfigSection.ClearFilters();
        else
            SqlCrudConfigSection.AddFilters(
                    new WsSqlFieldFilterModel
                    {
                        Name = nameof(WsSqlPluScaleModel.IsActive),
                        Value = true
                    }
                );
        SqlCrudConfigSection.AddFilters(nameof(WsSqlPluScaleModel.Line), Scale);
        base.SetSqlSectionCast();
        SqlSectionCast = SqlSectionCast.OrderBy(scale => scale.Plu.Number).ToList();
    }

    private string GetPluPackagesCount(WsSqlPluModel plu)
	{
		WsSqlCrudConfigModel sqlCrudConfig = WsSqlCrudConfigUtils.GetCrudConfig(plu, nameof(WsSqlPluScaleModel.Plu),
            WsSqlIsMarked.ShowAll, true, false, false);
        return ContextManager.AccessManager.AccessList.GetListNotNullable<WsSqlPluBundleFkModel>(sqlCrudConfig).Count.ToString();
	}

    protected override async Task OnSqlSectionSaveAsync()
    {
        await base.OnSqlSectionSaveAsync();
        SetSqlSectionCast();
    }

    #endregion
}
