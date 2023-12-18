
using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.Templates;

public sealed partial class TemplatesDataGrid : SectionDataGridBase<SqlTemplateEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateRepository TemplateRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        long.TryParse(SearchingSectionItemId, out long newLong);
        SqlTemplateEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueId == newLong, null);
        if (selectedEntity != null) await OpenSectionModal<TemplatesUpdateDialog>(selectedEntity);
    }

    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlTemplateEntity> e)
        => await OpenSectionModal<TemplatesUpdateDialog>(e.Item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateRepository.GetList(SqlCrudConfigSection);
}