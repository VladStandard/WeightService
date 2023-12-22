namespace DeviceControl.Pages.Menu.References.Templates;

public sealed partial class ItemTemplate : ItemBase<SqlTemplateEntity>
{
    private List<string> TemplateCategories { get; }

    public ItemTemplate() : base()
    {
        TemplateCategories =  new() {"Temp", "Temp"};
    }
}
