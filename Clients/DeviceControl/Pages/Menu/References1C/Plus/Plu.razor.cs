// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleModels.Plus;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class Plu : SectionBase<WsSqlPluModel>
{
    #region Public and private fields, properties, constructor

    public Plu() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlPluRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}