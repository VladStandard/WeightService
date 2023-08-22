// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Pages.Menu.References1C.Bundles;

public sealed partial class Bundles : SectionBase<WsSqlBundleModel>
{
    #region Public and private fields, properties, constructor

    public Bundles() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStaticSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new WsSqlBundleRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
