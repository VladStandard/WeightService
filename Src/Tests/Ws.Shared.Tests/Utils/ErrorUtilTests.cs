using Ws.Shared.Utils;

namespace Ws.Shared.Tests.Utils;

public class ErrorUtilTests
{
    [Fact]
    public void Throw_exception()
    {
        Action act = () => ErrorUtil.Suppress<ArgumentNullException>(() => throw new NotImplementedException());
        act.Should().Throw<NotImplementedException>();
    }

    [Fact]
    public void Suppress_exception() =>
        ErrorUtil.Suppress<ArgumentNullException>(() => throw new ArgumentNullException())
            .Should()
            .Be(true);

    [Fact]
    public void Suppress_without_exception() =>
        ErrorUtil.Suppress<ArgumentNullException>(() => throw new ArgumentNullException())
            .Should()
            .Be(true);

    [Fact]
    public void Suppress_any_exception()
    {
        ErrorUtil.Suppress(() => throw new ArgumentNullException(), typeof(ArgumentNullException),
                typeof(NotImplementedException))
            .Should()
            .Be(true);

        ErrorUtil.Suppress(() => throw new NotImplementedException(), typeof(ArgumentNullException),
                typeof(NotImplementedException))
            .Should()
            .Be(true);
    }

    [Fact]
    public void Suppress_not_any_exception()
    {
        Action act = () => ErrorUtil.Suppress(() => throw new TypeUnloadedException(), typeof(ArgumentNullException),
            typeof(NotImplementedException));
        act.Should().Throw<TypeUnloadedException>();
    }
}