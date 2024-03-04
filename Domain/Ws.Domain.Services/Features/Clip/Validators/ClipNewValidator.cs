namespace Ws.Domain.Services.Features.Clip.Validators;

internal sealed class ClipNewValidator : ClipValidator
{
    public ClipNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}