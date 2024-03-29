using DeviceControl2.Source.Shared.Localization;
using DeviceControl2.Source.Shared.Utils;
using DeviceControl2.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.ZplResource;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.PrintSettings.TemplateResources;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class TemplateResourcesPage : SectionDataGridPageBase<ZplResourceEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

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

    protected override Task DeleteItemAction(ZplResourceEntity item) {
        ZplResourceService.Delete(item);
        return Task.CompletedTask;
    }
}
