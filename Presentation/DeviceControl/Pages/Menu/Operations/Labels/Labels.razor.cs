using Ws.StorageCore.Entities.SchemaPrint.Labels;

namespace DeviceControl.Pages.Menu.Operations.Labels;

public sealed partial class Labels : SectionBase<SqlLabelEntity>
{
    public Labels() : base()
    {
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlLabelRepository().GetList(new());
    }
}
