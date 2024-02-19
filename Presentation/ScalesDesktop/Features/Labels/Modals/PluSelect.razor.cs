// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Features.Pallet.Resources;
using ScalesDesktop.Features.Shared;
using ScalesDesktop.Services;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Features.Labels.Modals;

public sealed partial class PluSelect : DataGridBase<PluEntity>
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<PalletResources> PalletLocalizer { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void GetGridData() => GridData = LabelContext.PluEntities;

    protected override async Task OnItemSelect(PluEntity obj)
    {
        LabelContext.ChangePlu(obj);
        await ModalService.Hide();
    }
}