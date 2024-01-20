using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.SchemaScale;
using Ws.StorageCore.Entities.Scales.TemplatesResources;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesDataGrid: SectionDataGridBase<TemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(TemplateResourceEntity item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);
    
    protected override async Task OpenItemInNewTab(TemplateResourceEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplateResources}/{item.IdentityValueUid.ToString()}");

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateResourceRepository.GetList();
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = [SqlCoreHelper.Instance.GetItemByUid<TemplateResourceEntity>(itemUid)];
    }
    
    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}
