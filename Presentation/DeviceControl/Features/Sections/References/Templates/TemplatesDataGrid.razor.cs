using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using DeviceControl.Utils;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Scale;
using Ws.Domain.Services.Features.Template;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesDataGrid : SectionDataGridBase<TemplateEntity>
{
    #region Region
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private ITemplateService TemplateService { get; set; } = null!;

    #endregion
    
    protected override async Task OpenDataGridEntityModal(TemplateEntity item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());
    
    protected override async Task OpenItemInNewTab(TemplateEntity item)
        => await OpenLinkInNewTab($"{RouteUtils.SectionTemplates}/{item.IdentityValueId.ToString()}");

    protected override IEnumerable<TemplateEntity> SetSqlSectionCast() => TemplateService.GetAll();
    
    protected override IEnumerable<TemplateEntity> SetSqlSearchingCast()
    {
        long.TryParse(SearchingSectionItemId, out long itemId);
        return [TemplateService.GetById(itemId)];
    }
}