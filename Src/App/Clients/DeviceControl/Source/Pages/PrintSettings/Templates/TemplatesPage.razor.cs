using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Shared.Utils;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.Template;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.PrintSettings.Templates;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class TemplatesPage : SectionDataGridPageBase<TemplateEntity>
{
    #region Region

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private ITemplateService TemplateService { get; set; } = default!;

    #endregion

    protected override async Task OpenDataGridEntityModal(TemplateEntity item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);

    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());

    protected override async Task OpenItemInNewTab(TemplateEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplates}/{item.Uid.ToString()}");

    protected override IEnumerable<TemplateEntity> SetSqlSectionCast() => TemplateService.GetAll();

    private string GetTemplateTypeName(bool isWeight) =>
        isWeight ? WsDataLocalizer["ColTemplateWeight"] : WsDataLocalizer["ColTemplatePiece"];

    protected override IEnumerable<TemplateEntity> SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemId);
        return [TemplateService.GetItemByUid(itemId)];
    }

    protected override Task DeleteItemAction(TemplateEntity item)
    {
        TemplateService.Delete(item);
        return Task.CompletedTask;
    }
}