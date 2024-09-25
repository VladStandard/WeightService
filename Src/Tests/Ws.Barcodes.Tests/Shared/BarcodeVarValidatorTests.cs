using FluentValidation.Results;
using Ws.Barcodes.Models;

namespace Ws.Barcodes.Tests.Shared;

public class BarcodeVarValidatorTests
{
    public static IEnumerable<object[]> ValidVariables => new List<object[]>
    {
        new object[] { nameof(BarcodeBuilder.PluEan13), "{0:000000000000000000000000}", false},
        new object[] { nameof(BarcodeBuilder.PluEan13), "{0:C}", false},
        new object[] { nameof(BarcodeBuilder.PluGtin), "{0:D13}", false},
        new object[] { nameof(BarcodeBuilder.PluEan13), "{0:D12}", false},
        new object[] { nameof(BarcodeBuilder.ExpirationDt), "{0:yyMMdd}", true},
        new object[] { nameof(BarcodeBuilder.ProductDt), "{0:yyMM}", true},
        new object[] { "", "{0:C}", false},
    };


    [Theory]
    [MemberData(nameof(ValidVariables))]
    public void Validate_PropertyDoesNotExist_ShouldBeInvalid(string property, string formatStr, bool result)
    {
        // Arrange
        BarcodeVar barcodeVar = new(property, formatStr);

        ValidationResult isValid = new BarcodeVarValidator().Validate(barcodeVar);

        isValid.IsValid.Should().Be(result);
    }
}