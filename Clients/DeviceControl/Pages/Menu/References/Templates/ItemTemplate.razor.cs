namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class ItemTemplate : ItemBase<WsSqlTemplateModel>
{
    #region Public and private fields, properties, constructor

    private List<string> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories = BlazorAppSettingsHelper.Instance.DataSourceDics.GetTemplateCategories();
    }

    #endregion
}
