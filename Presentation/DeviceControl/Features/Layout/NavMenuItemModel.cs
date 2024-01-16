namespace DeviceControl.Features.Layout;

public class NavMenuItemModel(string name, string link)
{
    public string Name { get; init; } = name;
    public string Link { get; init; } = link;
}