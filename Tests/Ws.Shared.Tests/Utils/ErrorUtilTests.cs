using Ws.Shared.Utils;

namespace Ws.Shared.Tests.Utils;

public class ErrorUtilTests
{
    [Fact]
    public void Throw_exception()
    {
        Assert.Throws<NotImplementedException>(
        () =>
        {
            ErrorUtil.Suppress<ArgumentNullException>(() => throw new NotImplementedException());
        });
    }

    [Fact]
    public void Suppress_exception()
    {
        Assert.True(ErrorUtil.Suppress<ArgumentNullException>(() => throw new ArgumentNullException()));
    }

    [Fact]
    public void Suppress_without_exception()
    {
        Assert.False(ErrorUtil.Suppress<ArgumentNullException>(() => { }));
    }

    [Fact]
    public void Suppress_any_exception()
    {
        Assert.True(ErrorUtil.Suppress(() => throw new ArgumentNullException(), typeof(ArgumentNullException),
        typeof(NotImplementedException)));
        Assert.True(ErrorUtil.Suppress(() => throw new NotImplementedException(), typeof(ArgumentNullException),
        typeof(NotImplementedException)));
    }
}