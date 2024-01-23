using Blazor.Heroicons;
using Blazorise;
using DeviceControl.Auth.Common;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Claim;

namespace DeviceControl.Features.Sections.Admin.PalletMen;

public sealed partial class PalletMenUpdateForm: SectionFormBase<PalletManEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private string Uid1C
    {
        get => SectionEntity.Uid1C.ToString();
        set { SectionEntity.Uid1C = Guid.TryParse(value, out Guid guid1C) ? guid1C : Guid.Empty; }
    }
}