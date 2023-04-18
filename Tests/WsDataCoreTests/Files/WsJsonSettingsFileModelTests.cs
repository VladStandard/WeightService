// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace WsDataCoreTests.Files;

public sealed class WsJsonSettingsFileModelTests
{
    #region Public and private methods

    [Test]
    public void JsonSettings_New_DoesNotThrow()
    {
        WsTestsUtils.DataTests.AssertAction(() =>
        {
            //
        }, false, new() { WsConfiguration.DevelopVS, WsConfiguration.ReleaseVS });
    }

    #endregion
}