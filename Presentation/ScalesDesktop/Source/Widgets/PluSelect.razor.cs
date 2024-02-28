// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesDesktop.Source.Shared.Localization;
using ScalesDesktop.Source.Shared.Services;
using ScalesDesktop.Source.Shared.UI;
using Ws.Domain.Models.Entities.Ref1c;
using Ws.SharedUI.Resources;

namespace ScalesDesktop.Source.Widgets;

public sealed partial class PluSelect : DataGridBase<PluEntity>
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStringLocalizer<Resources> PalletLocalizer { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void GetGridData() => GridData = LabelContext.PluEntities;

     protected override async Task OnItemSelect(PluEntity obj)
     {
         LabelContext.ChangePlu(obj);
         await ModalService.Hide();
     }
}