using DeviceControl.Resources;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Diag;

namespace DeviceControl.Features.Sections.Admin.Database;

public sealed partial class FileTablesDataGrid: ComponentBase
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    [Parameter] public IEnumerable<TableSizeEntity> FileTablesData { get; set; } = [];
}

