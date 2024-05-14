using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Source.Pages.PrintSettings.StorageMethods;

public sealed partial class StorageMethodsUpdateForm : SectionFormBase<StorageMethod>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = null!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = null!;

    #endregion

    protected override StorageMethod UpdateItemAction(StorageMethod item) =>
        StorageMethodService.Update(item);

    protected override Task DeleteItemAction(StorageMethod item)
    {
        StorageMethodService.Delete(item);
        return Task.CompletedTask;
    }
}

public class StorageMethodsUpdateFormValidator : AbstractValidator<StorageMethod>
{
    public StorageMethodsUpdateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}