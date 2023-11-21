using Ws.DataCore.Models;

namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class ItemTemplate : ItemBase<SqlTemplateEntity>
{
    #region Public and private fields, properties, constructor

    private List<string> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories =  DataSourceDicsHelper.Instance.GetTemplateCategories();
    }

    #endregion
}
