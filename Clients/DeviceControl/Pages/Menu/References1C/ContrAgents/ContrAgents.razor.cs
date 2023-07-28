// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DeviceControl.Settings;
using WsStorageCore.Tables.TableScaleModels.Contragents;

namespace DeviceControl.Pages.Menu.References1C.ContrAgents;

public sealed partial class ContrAgents : SectionBase<WsSqlContragentModel>
{
    #region Public and private fields, properties, constructor

    public ContrAgents() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlContragentRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}