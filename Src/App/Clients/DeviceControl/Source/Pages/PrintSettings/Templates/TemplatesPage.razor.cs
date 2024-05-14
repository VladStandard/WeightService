using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl.Source.Pages.PrintSettings.Templates;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class TemplatesPage : SectionDataGridPageBase<Template>
{
    #region Region

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(Template item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());

    protected override async Task OpenItemInNewTab(Template item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplates}/{item.Uid.ToString()}");

    protected override IEnumerable<Template> SetSqlSectionCast() => TemplateService.GetAll();

    private string GetTemplateTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColTemplateWeight"] : WsDataLocalizer["ColTemplatePiece"];

    protected override IEnumerable<Template> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemId);
        return [TemplateService.GetItemByUid(itemId)];
    }

    protected override Task DeleteItemAction(Template item)
    {
        TemplateService.Delete(item);
        return Task.CompletedTask;
    }
}