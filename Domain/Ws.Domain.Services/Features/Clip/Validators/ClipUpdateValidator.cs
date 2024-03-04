namespace Ws.Domain.Services.Features.Clip.Validators;

internal sealed class ClipUpdateValidator : ClipValidator
{
    public ClipUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}