using Blazorise;
using FluentValidation.Results;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Ws.StorageCore.Common;
using Ws.StorageCore.Helpers;
using Ws.StorageCore.Utils;

namespace DeviceControl.Features.Shared.Form;

public class SectionFormBase<TItem>: ComponentBase where TItem: SqlEntityBase, new()
{
    [Inject] private INotificationService NotificationService { get; set; } = null!;
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSubmitAction { get; set; }

    private TItem SectionEntityCopy { get; set; } = new();

    protected override Task OnInitializedAsync()
    {
        SectionEntityCopy = SectionEntity.DeepClone();
        return Task.CompletedTask;
    }

    protected async Task ResetItem()
    {
        // if (SectionEntity.Equals(SectionEntityCopy)) return;
        SectionEntity = SectionEntityCopy.DeepClone();
        await NotificationService.Info("Модель сброшена");
    }

    protected async Task SaveItem(TItem item)
    {
        if (!await IsValidateItem(item, false)) return;
        SqlCoreHelper.Instance.Save(item);
        await NotificationService.Success("Объект создан");
        await OnSubmitAction.InvokeAsync();
    }

    protected async Task UpdateItem(TItem item)
    {
        if (item.IsNew) return;
        if (!await IsValidateItem(item, true)) return;
        SqlCoreHelper.Instance.Update(item);
        await NotificationService.Success("Объект обновлен");
        await OnSubmitAction.InvokeAsync();
    }

    private async Task<bool> IsValidateItem(TItem item, bool isUpdateForm)
    {
        ValidationResult result = SqlValidationUtils.GetValidationResult(item, isUpdateForm);
        if (result.Errors.Count == 0) return true;
        foreach (ValidationFailure error in result.Errors)
            await NotificationService.Error(error.ErrorMessage);
        return false;
    }

    protected async Task DeleteItem()
    {
        SqlCoreHelper.Instance.Delete(SectionEntity);
        await OnSubmitAction.InvokeAsync();
    }
}