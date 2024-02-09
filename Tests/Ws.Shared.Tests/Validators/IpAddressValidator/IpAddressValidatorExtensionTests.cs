using FluentValidation.Results;

namespace Ws.Shared.Tests.Validators.IpAddressValidator;

public class IpAddressValidatorExtensionTests
{
    public static TheoryData<string, bool> ValidIpAddresses => new()
    {
        { "192.168.1.1", true },
        { "10.0.0.1", true },
        { "172.16.0.1", true },
        { "192.0.2.1", true },
        { "203.0.113.1", true },
        { "198.51.100.1", true },
        { "192.168.0.100", true },
        { "172.31.255.254", true },
        { "10.10.0.254", true },
        { "192.168.2.100", true }
    };

    public static TheoryData<string, bool> InvalidIpAddresses => new()
    {
        { "256.168.1.1", false },
        { "10.0.0.256", false },
        { "172.16.0.1.1", false },
        { "192.0.2.1.1", false },
        { "300.300.300.300", false },
        { "invalid.ip.address", false },
        { "192.168.1", false },
        { "192.168.1.1.1", false },
        { "192.168.1.-1", false },
        { "192.168.1.256", false }
    };

    [Theory]
    [MemberData(nameof(ValidIpAddresses))]
    [MemberData(nameof(InvalidIpAddresses))]
    public void Ip_Address_Validation(string ipAddress, bool expectedIsValid)
    {
        TestIpModel ip = new() { IpAddress = ipAddress };
        ValidationResult validatorRes = new TestModelValidator().Validate(ip);
        Assert.Equal(expectedIsValid, validatorRes.IsValid);
    }
}