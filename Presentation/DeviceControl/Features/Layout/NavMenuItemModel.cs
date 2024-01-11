using DeviceControl.Utils;

namespace DeviceControl.Features.Layout;

public class NavMenuItemModel
{
    public string Name { get; init; } = string.Empty;
    public string Link { get; init; } = RouteUtils.Home;
}