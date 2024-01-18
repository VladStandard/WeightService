using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Features.Shared;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace ScalesHybrid.Features.Labels.Dialogs;

public sealed partial class LineSelect : DataGridBase<SqlLineEntity>
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private LineContext LineContext { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;

    protected override void GetGridData() => GridData = LineContext.LineEntities;

    protected override async Task OnItemSelect(SqlLineEntity obj)
    {
        LineContext.ChangeLine(obj);
        await ModalService.Hide();
    }
}