using System.Text.RegularExpressions;
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

internal sealed partial class BarcodeItemValidator : AbstractValidator<BarcodeItemDto>
{
    [GeneratedRegex(@"^(\#\(\d{1,5}\)|\(\d{1,5}\)|\d{1,5})$")]
    private static partial Regex IsConstantRegex();

    public BarcodeItemValidator()
    {
        List<BarcodeVarDto> vars = BarcodeUtils.GetVariables();

        RuleFor(item => item.Property)
            .Must(i => IsConstantRegex().IsMatch(i) || vars.Any(j => j.Name == i));
    }
}