// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleModels.Versions;

namespace DeviceControl.Pages.Menu.Admins;

public sealed partial class Versions : SectionBase<WsSqlVersionModel>
{
    #region Public and private fields, properties, constructor

    public Versions() : base()
    {
        IsGuiShowFilterMarked = false;
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlVersionRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}