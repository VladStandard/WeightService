// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Files;

public class JsonSettingsFileEntityTests
{
    #region Public and private methods

    [Test]
    public void JsonSettings_New_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            TestContext.WriteLine($"{nameof(TestsUtils.DataAccess.JsonSettingsLocal)}: {TestsUtils.DataAccess.JsonSettingsLocal}");
        });

    }

    #endregion
}
