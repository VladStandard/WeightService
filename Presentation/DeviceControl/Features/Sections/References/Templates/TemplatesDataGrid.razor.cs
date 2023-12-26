
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
    
    protected override Func<SqlTemplateEntity, bool> SearchCondition =>
        item => item.IdentityValueId.ToString() == SearchingSectionItemId;

    protected override async Task OpenDataGridEntityModal(SqlTemplateEntity item)
        => await OpenSectionModal<TemplatesUpdateDialog>(item);
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<TemplatesCreateDialog>(new());

    protected override void SetSqlSectionCast() =>
        SectionItems = TemplateRepository.GetList(SqlCrudConfigSection);
}