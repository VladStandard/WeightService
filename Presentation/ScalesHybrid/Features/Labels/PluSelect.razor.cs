// ReSharper disable ClassNeverInstantiated.Global

using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using ScalesHybrid.Features.Shared;
using ScalesHybrid.Resources;
using ScalesHybrid.Services;
using Ws.StorageCore.Entities.SchemaRef1c.Plus;

namespace ScalesHybrid.Features.Labels;

public sealed partial class PluSelect : DataGridBase<SqlPluEntity>
{
    [Inject] private IModalService ModalService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Inject] private LabelContext LabelContext { get; set; } = null!;

    protected override void GetGridData() => GridData = LabelContext.PluEntities;

    protected override async Task OnItemSelect(SqlPluEntity obj)
    {
        LabelContext.ChangePlu(obj);
        await ModalService.Hide();
    }
}
