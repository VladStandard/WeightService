using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.TemplateResources;

public sealed partial class TemplateResourcesDataGrid: SectionDataGridBase<SqlTemplateResourceEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlTemplateResourceRepository TemplateResourceRepository { get; } = new();
    
    protected override async Task OpenSearchingEntityModal()
    {
        if (string.IsNullOrEmpty(SearchingSectionItemId)) return;
        Guid.TryParse(SearchingSectionItemId, out Guid newGuid);
        SqlTemplateResourceEntity? selectedEntity = SectionItems.FirstOrDefault(x => 
            x?.IdentityValueUid == newGuid, null);
        if (selectedEntity != null) await OpenSectionModal<TemplateResourcesUpdateDialog>(selectedEntity);
    }
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(DataGridRowMouseEventArgs<SqlTemplateResourceEntity> e)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(e.Item);

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateResourceRepository.GetList(SqlCrudConfigSection);
    
    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}
