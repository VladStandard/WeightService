namespace WsLabelCoreTests.Plugins.Helpers;

[TestFixture]
public sealed class PluginMemoryTests
{
    #region Public and private fields, properties, constructor

    private WsPluginMemoryHelper PluginMemory => WsPluginMemoryHelper.Instance;

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