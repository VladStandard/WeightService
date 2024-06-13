namespace Ws.Domain.Services.Features.Clips.Validators;

internal sealed class ClipUpdateValidator : ClipValidator
{
    public ClipUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}