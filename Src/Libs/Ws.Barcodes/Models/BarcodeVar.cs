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
        });

        When(x => !x.Property.IsDigitsOnly(), () =>
        {
            RuleFor(x => x)
                .Must(IsValidCustomPropertyFormat)
                .WithMessage("Invalid format for custom property.");
        });
    }

    #endregion

    #region Private

    private static readonly Dictionary<Type, object?> TypeDefaults = new()
    {
        { typeof(uint), 0u },
        { typeof(ushort), 0 },
        { typeof(string), "0" },
        { typeof(DateTime), DateTime.Now }
    };

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

        var propInfo = BarcodeVarUtils.BarcodeVarInfos.FirstOrDefault(info => info.Property == barcodeVar.Property);
        if (propInfo == null || mask.Contains(":C"))
            return false;

        if (!TypeDefaults.TryGetValue(propInfo.Type, out var defaultValue))
            return false;

        readyPropValue = defaultValue ?? barcodeVar.Property;
        oldMask = propInfo.Mask;
        return true;
    }

    private static bool TryValidateFormat(object readyPropValue, string formatStr, string oldMask)
    {
        try
        {
            string newFormattedValue = BarcodeVarUtils.GetVariableResult(readyPropValue, formatStr);

            if (string.IsNullOrEmpty(oldMask))
                return true;

            string oldFormattedValue = BarcodeVarUtils.GetVariableResult(readyPropValue, oldMask);
            return oldFormattedValue.Length <= newFormattedValue.Length;
        }
        catch
        {
            return false;
        }
    }


    #endregion

    #endregion
}