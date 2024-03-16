using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ZplResource;

namespace DeviceControl.Features.Sections.PrintSettings.TemplateResources;

public sealed partial class TemplateResourcesDataGrid : SectionDataGridBase<ZplResourceEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = null!;

    #endregion

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ZplResourceEntity item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ZplResourceEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplateResources}/{item.Uid.ToString()}");

    protected override IEnumerable<ZplResourceEntity> SetSqlSectionCast() => ZplResourceService.GetAll();

    protected override IEnumerable<ZplResourceEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ZplResourceService.GetItemByUid(itemUid)];
    }

    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}