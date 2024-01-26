using Blazorise;
using DeviceControl.Features.Sections.Shared.DataGrid;
using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities;
using Ws.Domain.Services.Features.DatabaseFile;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabaseFilesDataGrid: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private IModalService ModalService { get; set; } = null!;

    [Inject] private IDatabaseFileService DatabaseFileService { get; set; } = null!;
    
    private IEnumerable<DbFileSizeInfoEntity> DbFileSizesData { get; set; } = [];
    private SectionDataGridWrapper<DbFileSizeInfoEntity>? DataGridWrapperRef { get; set; }
    
    private async Task GetDbFilesData() => await Task.Run(() =>
        DbFileSizesData = DatabaseFileService.GetAll().Where(item => item.Tables.Count != 0));
    
        
    private async Task OpenTablesDataGridModal(DbFileSizeInfoEntity sectionEntity)
    {
        await ModalService.Show<DatabaseFilesDialog>(p =>
        {
            p.Add(x => x.DbFileData, sectionEntity);
            p.Add(x => x.OnDataChangedAction, new(this, OnModalSubmit));
        });
    }
    
    private async Task OnModalSubmit()
    {
        if (DataGridWrapperRef != null) await DataGridWrapperRef.ReloadData();
        await ModalService.Hide();
    }

    private static Color GetProgressColorByValue(int percentage) =>
        percentage switch
        {
            >= 80 => Color.Danger,
            >= 50 => Color.Warning,
            _ => Color.Success
        };
}
