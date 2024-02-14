using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Plu;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusUpdateForm : SectionFormBase<PluEntity>
{
    #region Inject

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

    private string GetBundleLink() => SectionEntity.Bundle.IsNew || SectionEntity.Bundle.Uid1C == Guid.Empty ?
        string.Empty : $"{RouteUtils.SectionBundles}/{SectionEntity.Bundle.Uid}";

    private string GetClipLink() => SectionEntity.Clip.IsNew || SectionEntity.Clip.Uid1C == Guid.Empty ?
        string.Empty : $"{RouteUtils.SectionClips}/{SectionEntity.Clip.Uid}";

    private string GetBrandLink() => SectionEntity.Brand.IsNew || SectionEntity.Brand.Uid1C == Guid.Empty ?
        string.Empty : $"{RouteUtils.SectionBrands}/{SectionEntity.Brand.Uid}";

    private string GetTemplateLink() => Template.IsNew ?
        string.Empty : $"{RouteUtils.SectionTemplates}/{Template.Uid}";

    private string GetStorageLink() => SectionEntity.StorageMethod.IsNew ?
        string.Empty : $"{RouteUtils.SectionStorageMethods}/{SectionEntity.StorageMethod.Uid}";
}