using Fluxor;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record PalletState(PalletInfo? Pallet)
{
    private PalletState() : this(Pallet: null) {}
}

public record ChangePalletAction(PalletInfo Pallet);

public class ChangePalletReducer : Reducer<PalletState, ChangePalletAction>
{
    public override PalletState Reduce(PalletState state, ChangePalletAction action) =>
        state.Pallet != null && state.Pallet.Equals(action.Pallet) ? state : new(action.Pallet);
}

public record ResetPalletAction;

public class ResetPalletReducer : Reducer<PalletState, ResetPalletAction>
{
    public override PalletState Reduce(PalletState state, ResetPalletAction action) => new(null);
}