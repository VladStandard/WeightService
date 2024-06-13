namespace Ws.Domain.Services.Features.Boxes.Validators;

internal sealed class BoxUpdateValidator : BoxValidator
{
    public BoxUpdateValidator()
    {
        RuleFor(item => item.IsExists).Equal(true);
    }
}