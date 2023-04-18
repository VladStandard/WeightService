// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen.Blazor;

namespace BlazorDeviceControl.Pages.CustomComponents;

public class RzDropDownLocal<T>: RadzenDropDown<T>
{
    public RzDropDownLocal()
    {
        Style = "width: 100%";
    }
}