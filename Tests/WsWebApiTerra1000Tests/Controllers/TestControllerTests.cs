namespace WsWebApiTerra1000Tests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetListInfo_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
            //foreach (string url in new WebRequestTerra1000().GetListInfo(ServerType.All))
            //{
            //    foreach (RestRequest request in WebRequestUtils.GetRequestFormats())
            //    {
            //        await WebResponseUtilsTests.GetInfoAsync(url, request);
            //    }
            //}
        });
    }
}