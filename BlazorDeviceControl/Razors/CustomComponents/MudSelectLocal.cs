// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Razors.CustomComponents;

public class MudSelectLocal<T> : MudSelect<T>
{
    public MudSelectLocal()
    {
        Margin = Margin.Dense;
        Variant =  MudBlazor.Variant.Outlined;
        AnchorOrigin = Origin.BottomCenter;
        Class = "MudSelect";
    }
}
