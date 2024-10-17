using Fluxor;
using Ws.Mobile.Models.Features.Users;

namespace ScalesMobile.Source.Shared.Stores;

[FeatureState]
public record UserState(UserDto? User)
{
    private UserState() : this(User: null) { }
}

public record ChangeUserAction(UserDto User);

public class ChangeUserReducer : Reducer<UserState, ChangeUserAction>
{
    public override UserState Reduce(UserState state, ChangeUserAction action) =>
        state.User?.Equals(action.User) == true ? state : new(action.User);
}