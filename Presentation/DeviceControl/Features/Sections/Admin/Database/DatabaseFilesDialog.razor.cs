using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities;
using Ws.Shared.Enums;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class DatabaseFilesDialog : ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter] public DbFileSizeInfoEntity DbFileData { get; set; } = null!;
    [Parameter] public EventCallback OnDataChangedAction { get; set; }
    
    private List<EnumTypeModel<string>> TabsList { get; set; } = [];

    protected override void OnInitialized() => TabsList = InitializeTabList();

    private List<EnumTypeModel<string>> InitializeTabList() =>
        [new(Localizer["DataGridColumnDatabaseTables"], "main")];
}