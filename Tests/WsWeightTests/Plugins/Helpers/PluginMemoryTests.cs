// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using WsWeightCore.Plugins.Helpers;

namespace WsWeightTests.Plugins.Helpers;

[TestFixture]
internal class PluginMemoryTests
{
    #region Public and private fields, properties, constructor

    private PluginMemoryHelper PluginMemory => PluginMemoryHelper.Instance;

    #endregion

    #region Public and private methods

    [Test]
    public void PluginMemory_GetMemorySizeAppMb_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluginMemory.MemorySize.Execute();
            short size = PluginMemory.GetMemorySizeAppMb();
            TestContext.WriteLine($"GetMemorySizeAppMb: {size} MB");
            Assert.That(size > 0);
        });
    }

    [Test]
    public void PluginMemory_GetMemorySizeFreeMb_DoesNotThrow()
    {
        Assert.DoesNotThrow(() =>
        {
            PluginMemory.MemorySize.Execute();
            short size = PluginMemory.GetMemorySizeFreeMb();
            TestContext.WriteLine($"GetMemorySizeFreeMb: {size} MB");
            Assert.That(size > 0);
        });
    }

    #endregion
}