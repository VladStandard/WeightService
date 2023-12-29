using DeviceControl.Features.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Brands;

namespace DeviceControl.Features.Sections.References1C.Brands;


public sealed partial class BrandsUpdateForm: SectionFormBase<SqlBrandEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
}
