// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace BlazorDeviceControl.Pages.CustomComponents;

public class MudNumericLocal<T> : MudNumericField<T>
{
    public MudNumericLocal()
    {
        Margin = Margin.Dense;
        Variant =  MudBlazor.Variant.Outlined;
    }
}
