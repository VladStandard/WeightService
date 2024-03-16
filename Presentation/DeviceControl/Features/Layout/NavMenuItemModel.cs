namespace DeviceControl.Features.Layout;

public class NavMenuItemModel(string name, string link, string? claim = null)
{
    public string Name { get; init; } = name;
    public string Link { get; init; } = link;
    public string? RequiredClaim { get; init; } = claim;
}