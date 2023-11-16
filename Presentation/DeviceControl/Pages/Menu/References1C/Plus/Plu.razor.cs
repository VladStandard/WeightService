using Ws.StorageCore.OrmUtils;

namespace DeviceControl.Pages.Menu.References1C.Plus;

public sealed partial class Plu : SectionBase<SqlPluEntity>
{
    #region Public and private fields, properties, constructor
    
    public Plu() : base()
    {
        SqlCrudConfigSection.AddFilter(SqlRestrictions.Equal(nameof(SqlPluEntity.IsGroup), false));
        ButtonSettings = ButtonSettingsModel.CreateForStatic1CSection();
    }
    
    protected override void SetSqlSectionCast()
    {
        SqlSectionCast = new SqlPluRepository().GetEnumerable(SqlCrudConfigSection).ToList();
    }

    #endregion
}
