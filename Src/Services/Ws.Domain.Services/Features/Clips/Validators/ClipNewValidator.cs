namespace Ws.Domain.Services.Features.Clips.Validators;

internal sealed class ClipNewValidator : ClipValidator
{
    public ClipNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}