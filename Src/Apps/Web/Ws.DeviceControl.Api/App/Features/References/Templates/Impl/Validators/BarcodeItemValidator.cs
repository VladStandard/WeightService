using FluentValidation;
using Ws.Print.Features.Barcodes.Models;
using Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Extensions;
using Ws.DeviceControl.Models.Features.References.Template.Universal;

namespace Ws.DeviceControl.Api.App.Features.References.Templates.Impl.Validators;

internal sealed class BarcodeItemWrapperValidator : AbstractValidator<BarcodeItemWrapper>
{
    public BarcodeItemWrapperValidator()
    {
        RuleForEach(i => i.Top.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Top));
        RuleForEach(i => i.Bottom.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Bottom));
        RuleForEach(i => i.Right.ToBarcodeVar()).SetValidator(new BarcodeVarValidator())
            .OverridePropertyName(nameof(BarcodeItemWrapper.Right));
    }
}