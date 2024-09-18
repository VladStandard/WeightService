using FluentValidation;
using Ws.DeviceControl.Api.App.Shared.Utils;
using Ws.DeviceControl.Models.Features.References.Template.Queries;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;

internal sealed class BarcodeItemWrapperValidator : AbstractValidator<BarcodeItemWrapper>
{
    public BarcodeItemWrapperValidator()
    {
        RuleForEach(i => i.Top).SetValidator(new BarcodeItemValidator());
        RuleForEach(i => i.Bottom).SetValidator(new BarcodeItemValidator());
        RuleForEach(i => i.Right).SetValidator(new BarcodeItemValidator());
    }
}

internal sealed class BarcodeItemValidator : AbstractValidator<BarcodeItemDto>
{
    public BarcodeItemValidator()
    {
        List<BarcodeVarDto> vars = BarcodeUtils.GetVariables();

        RuleFor(item => item.Property)
            .Must(i => i.IsDigitsOnly() || vars.Any(j => j.Name == i));

        RuleFor(item => item.FormatStr)
            .NotNull();
    }
}