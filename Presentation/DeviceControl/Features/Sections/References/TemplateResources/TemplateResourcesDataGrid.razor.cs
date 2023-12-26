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
    
    protected override Func<SqlTemplateResourceEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplateResourcesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlTemplateResourceEntity item)
        => await OpenSectionModal<TemplateResourcesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateResourceRepository.GetList(SqlCrudConfigSection);
    
    private static string ConvertBytes(int fileSize) =>
        fileSize > 1024 ? $"{fileSize / 1024:### ##0} Kb" : $"{fileSize:##0} bytes";
}
