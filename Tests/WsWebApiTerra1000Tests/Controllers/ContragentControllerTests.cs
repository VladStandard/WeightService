using System.Collections.Generic;
namespace WsWebApiTerra1000Tests.Controllers;

[TestFixture]
public sealed class ContragentControllerTests
{
    public static List<int> GetListContragentId => new()
    {
        -2147482738,
        -2147477242,
        -2147478355,
        -2147482505,
        -2147482782,
    };
    
    
    [Test]
    public void GetListContragent_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListContragent(WsSqlEnumServerType.All))
            {
                foreach (long id in GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
        TestContext.WriteLine();
    }

    [Test]
    public void GetListContragentV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListContragentV2(WsSqlEnumServerType.All))
            {
                foreach (long id in GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
    }

    private async Task GetContragentAsync(string url, string? code, long? id)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        await WsServiceUtilsResponse.GetResponseAsync(url, WsServiceUtilsRequest.GetRequestCodeOrId(code, id), (response) =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);

            if (!string.IsNullOrEmpty(response.Content))
            {
                if (code is not null)
                    Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", StringComparison.InvariantCultureIgnoreCase));
                if (id is not null)
                    Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", StringComparison.InvariantCultureIgnoreCase));
            }
        }).ConfigureAwait(false);
    }
}
