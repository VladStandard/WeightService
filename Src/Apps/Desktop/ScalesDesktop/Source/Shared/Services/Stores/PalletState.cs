using Fluxor;
using Ws.Desktop.Models.Features.Pallets.Output;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record PalletState(PalletInfo? Pallet, PalletViewTab PalletViewTab)
{
    private PalletState() : this(null, PalletViewTab.Info) {}
}

public record ChangePalletAction(PalletInfo Pallet);

public class ChangePalletReducer : Reducer<PalletState, ChangePalletAction>
{
    public override PalletState Reduce(PalletState state, ChangePalletAction action) =>
        state.Pallet != null && state.Pallet.Equals(action.Pallet) ? state : state with { Pallet = action.Pallet };
}

public record ResetPalletAction;

public class ResetPalletReducer : Reducer<PalletState, ResetPalletAction>
{
    public override PalletState Reduce(PalletState state, ResetPalletAction action) => state with { Pallet = null };
}

public record SwitchPalletDeleteFlagAction;

public class SwitchPalletDeleteFlagReducer : Reducer<PalletState, SwitchPalletDeleteFlagAction>
{
    public override PalletState Reduce(PalletState state, SwitchPalletDeleteFlagAction action)
    {
        PalletInfo? currentPallet = state.Pallet;
        if (currentPallet == null) return state;
        DateTime? newDeleteFlag = currentPallet.DeletedAt == null ? DateTime.Now : null;
        return state with { Pallet = currentPallet with { DeletedAt = newDeleteFlag } };
    }
}


public record ChangePalletViewTabAction(PalletViewTab Tab);

public class ChangePalletViewTabReducer : Reducer<PalletState, ChangePalletViewTabAction>
{
    public override PalletState Reduce(PalletState state, ChangePalletViewTabAction action) => state with { PalletViewTab = action.Tab };
}

public enum PalletViewTab
{
    Info,
    Labels
}