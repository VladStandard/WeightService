// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

namespace DataCoreTests.Files;

public class JsonSettingsFileModelTests
{
    #region Public and private fields, properties, constructor

    private DataCoreHelper DataCore { get; } = DataCoreHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void JsonSettings_New_DoesNotThrow()
    {
        DataCore.AssertAction(() =>
        {
            //
        });
    }

    #endregion
}
