using FluentValidation;
using Ws.Shared.Validators;

namespace Ws.Shared.Tests.Validators.IpAddressValidator;

public class TestIpModel
{
    public required string IpAddress { get; set; }
}

public class TestModelValidator : AbstractValidator<TestIpModel>
{
    public TestModelValidator()
    {
        RuleFor(x => x.IpAddress).MustBeAValidIpAddress();
    }
}