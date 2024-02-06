using Microsoft.AspNetCore.Components;
using ScalesDesktop.Services;

namespace ScalesDesktop.Features.Pallet.Modules;

public sealed partial class PalletSelect : ComponentBase, IDisposable
{
    [Inject] private PalletContext PalletContext { get; set; } = null!;

    // private int CurrentPage { get; set; } = 1;
    // private int MaxPageCount { get; set; } = 1;
    // private const int MaxItemsPerPage = 8;

    protected override void OnInitialized()
    {
        // MaxPageCount = (int)Math.Ceiling((decimal)PalletContext.PalletEntities.Count() / MaxItemsPerPage);
        PalletContext.OnStateChanged += StateHasChanged;
    }

    // private IEnumerable<PalletModel> GetFilteredPalletList() => 
    //     PalletContext.PalletEntities.Skip((CurrentPage - 1) * MaxItemsPerPage).Take(MaxItemsPerPage);
    //
    // private void SetPreviousPage() => CurrentPage = CurrentPage <= 1 ? 1 : CurrentPage - 1;
    //
    // private void SetNextPage() => CurrentPage = CurrentPage >= MaxPageCount ? MaxPageCount : CurrentPage + 1;

    public void Dispose() => PalletContext.OnStateChanged -= StateHasChanged;
}