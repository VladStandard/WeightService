using Ws.Desktop.Models.Features.PalletMen;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Services.Contexts;

public class PalletContext {
    public PalletInfo? Pallet { get; private set; }
    public PalletMan? PalletMan { get; private set; }

    public event Action? StateChanged;

    public void ChangePallet(PalletInfo? pallet)
    {
        if (Pallet != null && Pallet.Equals(pallet)) return;
        Pallet = pallet;
        StateChanged?.Invoke();
    }

    public void SetPalletMan(PalletMan palletMan)
    {
        PalletMan = palletMan;
        StateChanged?.Invoke();
    }

    public void ResetPalletMan()
    {
        PalletMan = null;
        StateChanged?.Invoke();
    }

    public void Reset()
    {
        Pallet = null;
        ResetPalletMan();
    }
}