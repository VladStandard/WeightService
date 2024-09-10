using Fluxor;
using Ws.Desktop.Models.Features.Plus.Weight.Output;

namespace ScalesDesktop.Source.Shared.Services.Stores;

[FeatureState]
public record PluState(PluWeight? Plu)
{
    private PluState() : this(Plu: null) { }
}

public record ChangePluAction(PluWeight Plu);

public class ChangePluReducer : Reducer<PluState, ChangePluAction>
{
    public override PluState Reduce(PluState state, ChangePluAction action) =>
        state.Plu != null && state.Plu.Equals(action.Plu) ? state : new(action.Plu);
}

public record ResetPluAction;

public class ResetPluReducer : Reducer<PluState, ResetPluAction>
{
    public override PluState Reduce(PluState state, ResetPluAction action) => new(null);
}