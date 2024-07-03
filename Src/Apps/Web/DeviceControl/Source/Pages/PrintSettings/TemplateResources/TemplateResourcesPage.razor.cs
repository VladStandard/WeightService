using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.ZplResources;
using Ws.Shared.Enums;

namespace DeviceControl.Source.Pages.PrintSettings.TemplateResources;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class TemplateResourcesPage : SectionDataGridPageBase<ZplResource>
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IZplResourceService ZplResourceService { get; set; } = default!;

    #endregion

    private ZplResourceType? Type { get; set; }

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());

    protected override async Task OpenDataGridEntityModal(ZplResource item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);

    protected override async Task OpenItemInNewTab(ZplResource item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplateResources}/{item.Uid.ToString()}");

    protected override IEnumerable<ZplResource> SetSqlSectionCast() => ZplResourceService.GetAll();

    protected override IEnumerable<ZplResource> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        return [ZplResourceService.GetItemByUid(itemUid)];
    }

    protected override Task DeleteItemAction(ZplResource item)
    {
        ZplResourceService.Delete(item.Uid, item.Name);
        return Task.CompletedTask;
    }
}