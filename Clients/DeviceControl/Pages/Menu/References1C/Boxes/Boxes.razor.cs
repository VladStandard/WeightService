// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsStorageCore.Tables.TableScaleModels.Boxes;

namespace DeviceControl.Pages.Menu.References1C.Boxes;

public sealed partial class Boxes : SectionBase<WsSqlBoxModel>
{
    #region Public and private fields, properties, constructor

    public Boxes() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlBoxRepository().GetList(SqlCrudConfigSection);
    }

    #endregion
}