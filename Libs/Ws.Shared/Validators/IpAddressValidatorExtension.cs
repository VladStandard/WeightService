using System.Net;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Ws.Shared.Validators;

public static partial class IpAddressValidatorExtension
{
        
    [GeneratedRegex(@"^((25[0-5]|(2[0-4]|1\d|[1-9]|)\d)\.?\b){4}$")]
    private static partial Regex MyRegex();
        
    public static void MustBeAValidIpAddress<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        ruleBuilder
            .NotEmpty().WithMessage("IP-адрес не должен быть пустым")
            .Must(BeAValidIpAddress).WithMessage("Введите корректный IP-адрес");
    }

    private static bool BeAValidIpAddress(string ipAddress)
    {
        return MyRegex().IsMatch(ipAddress) && IPAddress.TryParse(ipAddress, out _);
    }
}