using System.Reflection;
using System.Security.Claims;
using DeviceControl2.Source.Shared.Localization;
using Force.DeepCloner;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.FluentUI.AspNetCore.Components;
using Ws.Domain.Models.Common;
using Ws.Domain.Services.Exceptions;

namespace DeviceControl2.Source.Widgets.Section;

public abstract class SectionFormBase<TItem> : ComponentBase where TItem : EntityBase, new()
{
    [Inject] private IStringLocalizer<ApplicationResources> Localizer { get; set; } = default!;
    [Inject] private IToastService ToastService { get; set; } = default!;
    [Inject] private AuthenticationStateProvider AuthProvider { get; set; } = default!;
    
    [Parameter] public TItem SectionEntity { get; set; } = new();
    [Parameter] public EventCallback OnSubmitAction { get; set; }
    [Parameter] public EventCallback OnCancelAction { get; set; }
    
    protected ClaimsPrincipal User { get; private set; } = new();
    protected bool IsForceSubmit { get; set; }
    
    protected override async Task OnInitializedAsync() => 
        User = (await AuthProvider.GetAuthenticationStateAsync()).User;
    
    protected void ResetItem<TModel>(TModel model, TModel modelToCopy) where TModel : class
    {
        if (model.Equals(modelToCopy)) return;
        typeof(TModel).GetProperties().Where(property => property.CanWrite).ToList()
            .ForEach(property => property.SetValue(model, property.GetValue(modelToCopy)));
        ToastService.ShowInfo(Localizer["ToastResetItem"]);
    }
    
    protected async Task AddAction(Func<Task> action) =>
        await ExecuteAction(action, Localizer["ToastAddItem"]);
    
    protected async Task UpdateAction(Func<Task>action) =>
        await ExecuteAction(action, Localizer["ToastUpdateItem"]);
    
    protected async Task DeleteAction(Func<Task> action) =>
        await ExecuteAction(action, Localizer["ToastDeleteItem"]);
    
    private async Task ExecuteAction(Func<Task> action, string successMessage)
    {
        try
        {
            await action.Invoke();
            ToastService.ShowSuccess(successMessage);
            await OnSubmitAction.InvokeAsync();
        }
        catch (ValidateException ex)
        {
            foreach (string error in ex.Errors.Keys)
                ToastService.ShowWarning(ex.Errors[error]);
        }
        catch (DbServiceException)
        {
            ToastService.ShowError("Неизвестная ошибка. Попробуйте позже");
        }
    }
}