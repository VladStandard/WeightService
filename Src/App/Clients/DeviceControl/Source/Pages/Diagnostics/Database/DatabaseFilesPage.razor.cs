using DeviceControl.Source.Shared.Localization;
using DeviceControl.Source.Widgets.Section;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.JSInterop;
using Ws.Domain.Models.Entities;
using Ws.Domain.Services.Features.DatabaseFile;
using Ws.Shared.Resources;

namespace DeviceControl.Source.Pages.Diagnostics.Database;

// ReSharper disable ClassNeverInstantiated.Global
public sealed partial class DatabaseFilesPage : ComponentBase
{
    #region Inject

    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IDatabaseFileService DatabaseFileService { get; set; } = default!;
    [Inject] private IDialogService DialogService { get; set; } = default!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = default!;

    #endregion

    private IEnumerable<DbFileSizeInfoEntity> DbFileSizesData { get; set; } = [];
    private bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync() => await GetDatabaseData();

    private async Task GetDatabaseData()
    {
        if (IsLoading) return;

        IsLoading = true;
        StateHasChanged();

        DbFileSizesData = await Task.Run(() => DatabaseFileService.GetAll().Where(item => item.Tables.Count != 0));

        IsLoading = false;
        StateHasChanged();
    }

    private static async Task ContextFuncWrapper(DbFileSizeInfoEntity? item, EventCallback onComplete, Func<DbFileSizeInfoEntity, Task> action)
    {
        await onComplete.InvokeAsync();
        if (item == null) return;
        await action(item);
    }

    private async Task OpenDataGridEntityModal(DbFileSizeInfoEntity item)
        => await DialogService.ShowDialogAsync<DatabaseFilesDialog>(new SectionDialogContent<DbFileSizeInfoEntity> { Item = item },
            new()
            {
                OnDialogClosing = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                    await JsRuntime.InvokeVoidAsync("animateDialogClosing", instance.Id)
                ),
                OnDialogOpened = EventCallback.Factory.Create<DialogInstance>(this, async instance =>
                    await JsRuntime.InvokeVoidAsync("animateDialogOpening", instance.Id)
                )
            });
}