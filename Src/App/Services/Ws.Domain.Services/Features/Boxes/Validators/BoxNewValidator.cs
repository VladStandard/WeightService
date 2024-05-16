namespace Ws.Domain.Services.Features.Boxes.Validators;

internal sealed class BoxNewValidator : BoxValidator
{
    public BoxNewValidator()
    {
        RuleFor(item => item.IsNew).Equal(true);
    }
}