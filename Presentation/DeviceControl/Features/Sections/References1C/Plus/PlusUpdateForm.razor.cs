using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.PlusTemplatesFks;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusUpdateForm: SectionFormBase<PluEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private PluTemplateFkEntity PluTemplate { get; set; } = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        PluTemplate = new SqlPluTemplateFkRepository().GetItemByPlu(SectionEntity);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];

    private string GetBundleLink() => SectionEntity.Bundle.IsNew ? 
        string.Empty : $"{RouteUtils.SectionBundles}/{SectionEntity.Bundle.IdentityValueUid}";
    
    private string GetBrandLink() => SectionEntity.Brand.IsNew ? 
        string.Empty : $"{RouteUtils.SectionBrands}/{SectionEntity.Brand.IdentityValueUid}";
    
    private string GetTemplateLink() => PluTemplate.Template.IsNew ? 
        string.Empty : $"{RouteUtils.SectionTemplates}/{PluTemplate.Template.IdentityValueId}";
    
    private string GetStorageLink() => SectionEntity.StorageMethod.IsNew ?
        string.Empty : $"{RouteUtils.SectionStorageMethods}/{SectionEntity.StorageMethod.IdentityValueUid}";
}
