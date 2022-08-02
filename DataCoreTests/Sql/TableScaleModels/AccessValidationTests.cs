// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCore.Sql.TableScaleModels;
using FluentValidation.Results;
using NSubstitute;
using NUnit.Framework;
using System;
using static DataCore.ShareEnums;

namespace DataCoreTests.Sql.TableScaleModels;

[TestFixture]
internal class AccessValidationTests
{
    [Test]
    public void Entity_Validate_IsFalse()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            AccessEntity access = Substitute.For<AccessEntity>();
            AccessValidation validation = new();
            // Act.
            ValidationResult result = validation.Validate(access);
            // Assert.
            Assert.IsFalse(result.IsValid);
            foreach (ValidationFailure? failure in result.Errors)
            {
                TestContext.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }
            // Act.
            access.User = "Test";
            access.Rights = (byte)AccessRights.Admin + 1;
            result = validation.Validate(access);
            // Assert.
            Assert.IsFalse(result.IsValid);
            foreach (ValidationFailure? failure in result.Errors)
            {
                TestContext.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
            }
        });
    }

    [Test]
    public void Entity_Validate_IsTrue()
    {
        Assert.DoesNotThrow(() =>
        {
            // Arrange.
            AccessEntity access = Substitute.For<AccessEntity>();
            AccessValidation validation = new();
            // Act.
            access.User = "Test";
            foreach (AccessRights rights in Enum.GetValues(typeof(AccessRights)))
            {
                access.Rights = (byte)rights;
                ValidationResult result = validation.Validate(access);
                TestContext.WriteLine(access);
                // Assert.
                Assert.IsTrue(result.IsValid);
            }
        });
    }
}
