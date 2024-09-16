using Ws.Desktop.Models.Features.PalletMen;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record PalletManState(PalletMan? PalletMan)
{
    private PalletManState() : this(PalletMan: null) { }
}

public record ChangePalletManAction(PalletMan PalletMan);

public class ChangePalletManReducer : Reducer<PalletManState, ChangePalletManAction>
{
    public override PalletManState Reduce(PalletManState state, ChangePalletManAction action) =>
        state.PalletMan != null && state.PalletMan.Equals(action.PalletMan) ? state : new(action.PalletMan);
}

public record ResetPalletManAction;

public class ResetPalletManReducer : Reducer<PalletManState, ResetPalletManAction>
{
    public override PalletManState Reduce(PalletManState state, ResetPalletManAction action) => new(null);
}