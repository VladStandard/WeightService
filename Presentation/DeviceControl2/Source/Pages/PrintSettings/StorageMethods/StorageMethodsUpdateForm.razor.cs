using DeviceControl2.Source.Widgets.Section;
using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Ws.Domain.Models.Entities.Ref;
using Ws.Domain.Services.Features.StorageMethod;
using Ws.Shared.Resources;

namespace DeviceControl2.Source.Pages.PrintSettings.StorageMethods;

public sealed partial class StorageMethodsUpdateForm: SectionFormBase<StorageMethodEntity>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = null!;

    #endregion

    protected override StorageMethodEntity UpdateItemAction(StorageMethodEntity item) =>
        StorageMethodService.Update(item);

    protected override Task DeleteItemAction(StorageMethodEntity item)
    {
        StorageMethodService.Delete(item);
        return Task.CompletedTask;
    }
}

public class StorageMethodsUpdateFormValidator : AbstractValidator<StorageMethodEntity>
{
    public StorageMethodsUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}
