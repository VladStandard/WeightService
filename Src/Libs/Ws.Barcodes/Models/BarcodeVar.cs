using FluentValidation;
using Ws.Barcodes.Utils;
using Ws.Shared.Extensions;

namespace Ws.Barcodes.Models;

public sealed record BarcodeVar(string Property, string FormatStr);

public class BarcodeVarValidator : AbstractValidator<BarcodeVar>
{
    #region Public

    public BarcodeVarValidator()
    {
        RuleFor(x => x.Property)
            .NotEmpty()
            .MaximumLength(20)
            .Must(IsValidPropertyOrDigits);

        RuleFor(x => x.FormatStr)
            .NotEmpty();

        When(x => x.Property.IsDigitsOnly(), () =>
        {
            RuleFor(x => x.FormatStr)
                .Must(IsValidDigitsOnlyFormat);
        }).Otherwise(
            () =>
            RuleFor(x => x)
                .Must(IsValidCustomPropertyFormat)
        );
    }

    #endregion

    #region Private

    #region IsValid

    private static bool IsValidPropertyOrDigits(string property) =>
        BarcodeVarUtils.BarcodeVarInfos.Any(info => info.Property == property) || property.IsDigitsOnly();

    private static bool IsValidDigitsOnlyFormat(string formatStr) =>
        BarcodeVarUtils.BarcodeVarConstantsFormats.Contains(formatStr);

    private static bool IsValidCustomPropertyFormat(BarcodeVar barcodeVar)
    {
        return TryGetReadyPropValue(barcodeVar, barcodeVar.FormatStr, out var readyPropValue, out var oldMask) &&
               TryValidateFormat(readyPropValue, barcodeVar.FormatStr, oldMask);
    }

    #endregion

    #region Try

    private static bool TryGetReadyPropValue(BarcodeVar barcodeVar, string mask, out object readyPropValue, out string oldMask)
    {
        oldMask = string.Empty;
        readyPropValue = barcodeVar.Property;

        BarcodeVarInfo? propInfo = BarcodeVarUtils.BarcodeVarInfos.FirstOrDefault(info => info.Property == barcodeVar.Property);
        if (propInfo == null || mask.Contains(":C"))
            return false;

        readyPropValue = propInfo.Example;
        oldMask = propInfo.Mask;
        return true;
    }

    private static bool TryValidateFormat(object readyPropValue, string formatStr, string oldMask)
    {
        if (!BarcodeVarUtils.TryFormat(readyPropValue, formatStr, out string? newFormattedValue))
            return false;

        if (string.IsNullOrEmpty(oldMask))
            return true;

        if (readyPropValue is DateTime) return true;

        if (!BarcodeVarUtils.TryFormat(readyPropValue, oldMask, out string? oldFormattedValue))
            return false;

        return oldFormattedValue.Length <= newFormattedValue.Length;
    }

    #endregion

    #endregion
}