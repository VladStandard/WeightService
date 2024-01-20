using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.Templates;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesDataGrid : SectionDataGridBase<TemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateRepository TemplateRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(TemplateEntity item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());
    
    protected override async Task OpenItemInNewTab(TemplateEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplates}/{item.IdentityValueId.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateRepository.GetList(new());
    
    protected override void SetSqlSearchingCast()
    {
        long.TryParse(SearchingSectionItemId, out long itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemById<TemplateEntity>(itemUid)];
    }
}