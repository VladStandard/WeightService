using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Scale;
using Ws.Services.Features.Plu;

namespace DeviceControl.Features.Sections.Operations.Labels;


public sealed partial class LabelsUpdateForm: SectionFormBase<LabelEntity>
{
    #region MyRegion

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;
    
    #endregion

    private TemplateEntity Template { get; set; } = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        Template = PluService.GetPluTemplate(SectionEntity.Pallet.Plu);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];
    
    private string GetTemplateLink() => Template.IsNew ? 
        string.Empty : $"{RouteUtils.SectionTemplates}/{Template.IdentityValueId}";
    
    private string GetLineLink() => SectionEntity.Pallet.Line.IsNew ? 
        string.Empty : $"{RouteUtils.SectionLines}/{SectionEntity.Pallet.Line.IdentityValueUid}";
    
    private string GetPluLink() => SectionEntity.Pallet.Plu.IsNew ? 
        string.Empty : $"{RouteUtils.SectionPlus}/{SectionEntity.Pallet.Plu.IdentityValueUid}";
}