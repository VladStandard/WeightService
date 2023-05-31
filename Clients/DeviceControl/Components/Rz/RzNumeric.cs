// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using Radzen.Blazor;

namespace DeviceControl.Components.Rz;

public class RzNumeric<T> : RadzenNumeric<T>
{
    public RzNumeric()
    {
        Style = "width: 100%";
    }
}
