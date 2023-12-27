
using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.StorageCore.Helpers;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesDataGrid : SectionDataGridBase<SqlTemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateRepository TemplateRepository { get; } = new();

    protected override async Task OpenDataGridEntityModal(SqlTemplateEntity item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateRepository.GetList(SqlCrudConfigSection);
    
    protected override void SetSqlSearchingCast()
    {
        Guid.TryParse(SearchingSectionItemId, out Guid itemUid);
        SectionItems = new() { SqlCoreHelper.Instance.GetItemByUid<SqlTemplateEntity>(itemUid) };
    }
}