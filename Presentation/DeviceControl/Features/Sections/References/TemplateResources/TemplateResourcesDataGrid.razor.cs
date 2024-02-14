using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.ZplResource;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesDataGrid : SectionDataGridBase<TemplateResourceEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(TemplateResourceEntity item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(TemplateResourceEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplateResources}/{item.Uid.ToString()}");

    protected override IEnumerable<TemplateResourceEntity> SetSqlSectionCast() => ZplResourceService.GetAll();

    protected override IEnumerable<TemplateResourceEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ZplResourceService.GetItemByUid(itemUid)];
    }

    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}