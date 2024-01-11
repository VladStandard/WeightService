using DeviceControl.Features.Sections.Shared.Form;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Entities.SchemaPrint.Labels;
using Ws.StorageCore.Entities.SchemaScale.PlusTemplatesFks;

namespace DeviceControl.Features.Sections.Operations.Labels;


public sealed partial class LabelsUpdateForm: SectionFormBase<SqlLabelEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    private SqlPluTemplateFkEntity PluTemplate { get; set; } = new();

    protected override void OnAfterRender(bool firstRender)
    {
        if (!firstRender) return;
        PluTemplate = new SqlPluTemplateFkRepository().GetItemByPlu(SectionEntity.Pallet.Plu);
    }

    private string GetPluTypeTitle(bool isWeight) =>
        isWeight ? Localizer["DataGridColumnIsWeight"] : Localizer["DataGridColumnIsPiece"];
    
    private string GetTemplateLink() => PluTemplate.Template.IsNew ? 
        string.Empty : $"{RouteUtils.SectionTemplates}/{PluTemplate.Template.IdentityValueId}";
    
    private string GetLineLink() => SectionEntity.Pallet.Line.IsNew ? 
        string.Empty : $"{RouteUtils.SectionLines}/{SectionEntity.Pallet.Line.IdentityValueUid}";
    
    private string GetPluLink() => SectionEntity.Pallet.Plu.IsNew ? 
        string.Empty : $"{RouteUtils.SectionPlus}/{SectionEntity.Pallet.Plu.IdentityValueUid}";
}