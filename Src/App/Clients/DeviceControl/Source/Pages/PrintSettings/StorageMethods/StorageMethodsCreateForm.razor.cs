using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Services.Features.StorageMethod;

namespace DeviceControl.Source.Pages.PrintSettings.StorageMethods;

public sealed partial class StorageMethodsCreateForm : SectionFormBase<StorageMethod>
{
    #region Inject

    [Inject] private IStringLocalizer<WsDataResources> WsDataLocalizer { get; set; } = default!;
    [Inject] private IStorageMethodService StorageMethodService { get; set; } = default!;

    #endregion

    protected override StorageMethod CreateItemAction(StorageMethod item) =>
        StorageMethodService.Create(item);
}

public class StorageMethodsCreateFormValidator : AbstractValidator<StorageMethod>
{
    public StorageMethodsCreateFormValidator()
    {
        RuleFor(item => item.Name).NotEmpty();
        RuleFor(item => item.Zpl).NotEmpty();
    }
}