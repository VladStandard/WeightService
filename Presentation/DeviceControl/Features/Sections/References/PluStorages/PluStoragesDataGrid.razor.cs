using Blazorise.DataGrid;
using DeviceControl.Features.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;

namespace DeviceControl.Features.Sections.References.PluStorages;

public sealed partial class PluStoragesDataGrid : SectionDataGridBase<SqlPluStorageMethodEntity>
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    
    private SqlPluStorageMethodRepository PluStorageRepository { get; } = new();
    
    protected override Func<SqlPluStorageMethodEntity, bool> SearchCondition =>
        item => item.IdentityValueUid.ToString() == SearchingSectionItemId;
    
    protected override async Task OpenSectionCreateForm()
        => await OpenSectionModal<PluStoragesCreateDialog>(new());
    
    protected override async Task OpenDataGridEntityModal(SqlPluStorageMethodEntity item)
        => await OpenSectionModal<PluStoragesUpdateDialog>(item);

    protected override void SetSqlSectionCast() =>
        SectionItems = PluStorageRepository.GetList(SqlCrudConfigSection);
}