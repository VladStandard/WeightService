// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DeviceControl.Components.Rz;

public class RzDropDown<T> : RadzenDropDown<T>
{
    public RzDropDown()
    {
        Style = "width: 100%";
    }
}