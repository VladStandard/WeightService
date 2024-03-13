using DeviceControl.Auth.Claims;
using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.Operations.Labels;

public sealed partial class LabelsUpdateForm : SectionFormBase<LabelEntity>
{
    #region MyRegion

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IPluService PluService { get; set; } = null!;

    #endregion

    private TemplateEntity Template { get; set; } = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        Template = PluService.GetPluTemplate(SectionEntity.Plu);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];

    private string GetTemplateLink() => Template.IsNew || !UserHasClaim(PolicyNameUtils.Admin)?
        string.Empty : $"{RouteUtils.SectionTemplates}/{Template.Uid}";

    private string GetLineLink() => SectionEntity.Line.IsNew || !UserHasClaim(PolicyNameUtils.Support) ?
        string.Empty : $"{RouteUtils.SectionLines}/{SectionEntity.Line.Uid}";

    private string GetPluLink() => SectionEntity.Plu.IsNew ?
        string.Empty : $"{RouteUtils.SectionPlus}/{SectionEntity.Plu.Uid}";
}