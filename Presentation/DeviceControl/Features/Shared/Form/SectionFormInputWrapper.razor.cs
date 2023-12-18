using Microsoft.AspNetCore.Components;

namespace DeviceControl.Features.Shared.Form;

public sealed partial class SectionFormInputWrapper: SectionFormInputBase
{
    [Parameter] public RenderFragment? ChildContent { get; set; }
    [Parameter] public SectionFormInputSizeEnum Size { get; set; } = SectionFormInputSizeEnum.Small;

    private string GetClassBySize(SectionFormInputSizeEnum size) =>
        size switch
        {
            SectionFormInputSizeEnum.Medium => "h-14 w-full",
            SectionFormInputSizeEnum.Large => "h-28 w-full",
            SectionFormInputSizeEnum.ExtraLarge => "h-56 w-full",
            _ => "h-14 w-1/2"
        };
    
}