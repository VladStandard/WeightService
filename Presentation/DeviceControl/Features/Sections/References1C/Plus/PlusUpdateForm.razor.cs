using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;
using Ws.StorageCore.Entities.SchemaScale.PlusStorageMethodsFks;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace DeviceControl.Features.Sections.References1C.Plus;

public sealed partial class PlusUpdateForm: SectionFormBase<SqlPluEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlPluStorageMethodFkEntity PluStorage { get; set; } = new();
    private SqlPluTemplateFkEntity PluTemplate { get; set; } = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        PluTemplate = new SqlPluTemplateFkRepository().GetItemByPlu(SectionEntity);
        PluStorage = new SqlPluStorageMethodFkRepository().GetItemByPlu(SectionEntity);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];

    private string GetBundleLink() => SectionEntity.Bundle.IsNew ? 
        string.Empty : $"{RouteUtils.SectionBundles}/{SectionEntity.Bundle.IdentityValueUid}";
    
    private string GetBrandLink() => SectionEntity.Brand.IsNew ? 
        string.Empty : $"{RouteUtils.SectionBrands}/{SectionEntity.Brand.IdentityValueUid}";
    
    private string GetTemplateLink() => PluTemplate.Template.IsNew ? 
        string.Empty : $"{RouteUtils.SectionTemplates}/{PluTemplate.Template.IdentityValueId}";
    
    private string GetStorageLink() => PluStorage.Method.IsNew ?
        string.Empty : $"{RouteUtils.SectionPlusStorage}/{PluStorage.Method.IdentityValueUid}";
}
