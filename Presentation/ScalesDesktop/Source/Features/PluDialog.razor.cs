using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.Shared.Resources;

namespace ScalesDesktop.Source.Features;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class PluDialog : ComponentBase, IDialogContentComponent<PluDialogContent>
{
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Parameter] public PluDialogContent Content { get; set; } = default!;
    [CascadingParameter] public FluentDialog Dialog { get; set; } = default!;
    
    private PaginationState Pagination { get; } = new() { ItemsPerPage = 7 };

    private async Task HandleOnRowFocus(FluentDataGridRow<PluEntity> obj) =>
        await Dialog.CloseAsync(obj.Item);
}

public record PluDialogContent
{
    public IQueryable<PluEntity> Data { get; init; } = new List<PluEntity>().AsQueryable();
}