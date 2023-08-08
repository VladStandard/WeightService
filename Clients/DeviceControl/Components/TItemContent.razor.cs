// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
namespace DeviceControl.Components;

public partial class TItemContent<TItem>
{
    [Parameter] public RenderFragment ChildContent { get; set; }
}