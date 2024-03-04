using Blazorise;
using DeviceControl.Resources;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Common;
using Ws.Domain.Services.Exceptions;

namespace DeviceControl.Features.Sections.Shared.Form;

public class SectionFormBase<TItem> : ComponentBase where TItem : EntityBase, new()
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = null!;
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSubmitAction { get; set; }

    private TItem SectionEntityCopy { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        SectionEntityCopy = SectionEntity.DeepClone();
        return Task.CompletedTask;
    }

    protected string GetMockIfEmptyInput(string value) =>
        string.IsNullOrEmpty(value) ? Localizer["SectionFormInputEmpty"] : value;

    protected async Task ResetItem()
    {
        if (SectionEntity.Equals(SectionEntityCopy)) return;
        SectionEntity = SectionEntityCopy.DeepClone();
        await NotificationService.Info(Localizer["ToastResetItem"]);
    }

    protected async Task AddItem(TItem item, Func<TItem, TItem> saveAction)
    {
        try
        {
            saveAction(item);
            await NotificationService.Success(Localizer["ToastAddItem"]);
            await OnSubmitAction.InvokeAsync();
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                await NotificationService.Warning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            await NotificationService.Error("Неизвестная ошибка. Попробуйте позже");
        }
    }

    protected async Task UpdateItem(TItem item, Func<TItem, TItem> updateAction)
    {
        if (!SectionEntity.Equals(SectionEntityCopy))
        {
            try
            {
                updateAction(item);
                await NotificationService.Success(Localizer["ToastUpdateItem"]);
            }
            catch (ValidateException ex)
            {
                foreach (string error in ex.Errors.Keys)
                    await NotificationService.Warning(ex.Errors[error]);
                return;
            }
            catch (DbServiceException)
            {
                await NotificationService.Error("Неизвестная ошибка. Попробуйте позже");
                return;
            }
        }
        await OnSubmitAction.InvokeAsync();
    }

    // TODO: localization
    protected async Task DeleteItem(Action<TItem> deleteItem)
    {
        try
        {
            deleteItem(SectionEntity);
            await NotificationService.Success(Localizer["ToastDeleteItem"]);
            await OnSubmitAction.InvokeAsync();
        }
        catch (DbServiceException)
        {
            await NotificationService.Error("Удаление не возможно. Запись используется");
        }
    }
}