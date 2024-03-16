using DeviceControl.Auth.Claims;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusUpdateForm : SectionFormBase<PluEntity>
{
    #region Inject
    [Inject] private RedirectUtils RedirectUtils { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    #endregion

    private TemplateEntity Template { get; set; } = new();
    
    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        Template = PluService.GetPluTemplate(SectionEntity);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];
}