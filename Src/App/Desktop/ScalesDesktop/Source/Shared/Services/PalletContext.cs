using Ws.Desktop.Models.Features.PalletMen;

namespace ScalesDesktop.Source.Shared.Services;

public class PalletContext {
    // public ViewPallet CurrentPallet { get; private set; } = new();
    // public IEnumerable<ViewPallet> PalletEntities { get; private set; } = [];
    public PalletMan? PalletMan { get; private set; }

    public event Action? StateChanged;

    // public void InitializeContext()
    // {
    //     PalletMan = new();
    //     UpdatePalletData();
    // }
    //
    // public void UpdatePalletData()
    // {
    //     CurrentPallet = new();
    //     PalletEntities = GetPallets();
    //     StateChanged?.Invoke();
    // }

    public void SetPalletMan(PalletMan palletMan)
    {
        PalletMan = palletMan;
        StateChanged?.Invoke();
    }

    // public void ResetPalletMan()
    // {
    //     PalletMan = new();
    //     StateChanged?.Invoke();
    // }

    // private IEnumerable<ViewPallet> GetPallets() => [];

    // private IEnumerable<ViewPallet> GetPallets() => palletService.GetAllViewByWarehouse(lineContext.Line.Warehouse);

    // public void ChangePallet(ViewPallet palletView)
    // {
    //     if (CurrentPallet.Equals(palletView)) return;
    //     CurrentPallet = palletView;
    //     StateChanged?.Invoke();
    // }
}