using Fluxor;
using Ws.Domain.Models.Entities.Ref;

namespace DeviceControl.Source.Shared.Services;

[FeatureState]
public record ProductionSiteState(ProductionSite ProductionSite)
{
    private ProductionSiteState() : this(ProductionSite: new()) {}
}

public record ChangeProductionSiteAction(ProductionSite ProductionSite);

public class ChangeProductionSiteReducer : Reducer<ProductionSiteState, ChangeProductionSiteAction>
{
    public override ProductionSiteState Reduce(ProductionSiteState state, ChangeProductionSiteAction action) =>
        state.ProductionSite.IsExists && state.ProductionSite.Equals(action.ProductionSite) ? state : new(action.ProductionSite);
}

public record ResetProductionSiteAction;

public class ResetProductionSiteReducer : Reducer<ProductionSiteState, ResetProductionSiteAction>
{
    public override ProductionSiteState Reduce(ProductionSiteState state, ResetProductionSiteAction action) => new(new());
}