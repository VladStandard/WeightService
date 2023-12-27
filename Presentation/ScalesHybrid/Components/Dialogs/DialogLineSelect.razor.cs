using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaRef.Lines;

namespace ScalesHybrid.Components.Dialogs;

public sealed partial class DialogLineSelect : ComponentBase
{
    [Inject] private LineContext LineContext { get; set; }
    [Inject] public IModalService ModalService { get; set; }
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; }

    private IEnumerable<SqlLineEntity> GetLineEntities() => LineContext.LineEntities;

    private async void OnRowSelected(SqlLineEntity obj)
    {
        LineContext.ChangeLine(obj);
        await ModalService.Hide();
    }
}