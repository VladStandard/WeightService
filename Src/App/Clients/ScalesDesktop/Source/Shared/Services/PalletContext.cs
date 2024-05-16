using Ws.Domain.Models.Entities.Print;
using Ws.Domain.Models.Entities.Users;
using Ws.Domain.Services.Features.Pallets;

namespace ScalesDesktop.Source.Shared.Services;

public class PalletContext(LineContext lineContext, IPalletService palletService)
{
    public ViewPallet CurrentPallet { get; private set; } = new();
    public IEnumerable<ViewPallet> PalletEntities { get; private set; } = [];
    public PalletMan PalletMan { get; private set; } = new();

    public event Action? OnStateChanged;

    public void InitializeContext()
    {
        PalletMan = new();
        UpdatePalletData();
    }

    public void UpdatePalletData()
    {
        CurrentPallet = new();
        PalletEntities = GetPallets();
        OnStateChanged?.Invoke();
    }

    public void SetPalletMan(PalletMan palletMan)
    {
        PalletMan = palletMan;
        OnStateChanged?.Invoke();
    }

    public void ResetPalletMan()
    {
        PalletMan = new();
        OnStateChanged?.Invoke();
    }

    private IEnumerable<ViewPallet> GetPallets() => palletService.GetAllViewByWarehouse(lineContext.Line.Warehouse);

    public void ChangePallet(ViewPallet palletView)
    {
        if (CurrentPallet.Equals(palletView)) return;
        CurrentPallet = palletView;
        OnStateChanged?.Invoke();
    }
}