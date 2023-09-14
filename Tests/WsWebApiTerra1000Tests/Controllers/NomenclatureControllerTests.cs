using System.Collections.Generic;
namespace WsWebApiTerra1000Tests.Controllers;

[TestFixture]
public sealed class NomenclatureControllerTests
{
    
    public static List<int> GetListNomenclatureId => new()
    {
        -2147460739,
        -2147440723,
        -2147460730,
        -2147464402,
        -2147464403,
    };
    
    [Test]
    public void GetListNomenclature_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListNomenclature(WsSqlEnumServerType.All))
            {
                foreach (long id in GetListNomenclatureId)
                {
                    await GetNomenclatureAsync(url, null, id).ConfigureAwait(false);
                    TestContext.WriteLine();
                }
            }
        });
        TestContext.WriteLine();
    }

    [Test]
    public void GetListNomenclatureV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListNomenclatureV2(WsSqlEnumServerType.All))
            {
                foreach (long id in GetListNomenclatureId)
                {
                    await GetNomenclatureAsync(url, null, id);
                    TestContext.WriteLine();
                }
            }
        });
    }

    private async Task GetNomenclatureAsync(string url, string? code, long? id)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(1)).ConfigureAwait(false);
        await WsServiceUtilsResponse.GetResponseAsync(url, WsServiceUtilsRequest.GetRequestCodeOrId(code, id), (response) =>
        {
            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            
            if (!string.IsNullOrEmpty(response.Content))
            {
                if (code != null)
                    Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", StringComparison.InvariantCultureIgnoreCase));
                else if (id != null)
                    Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", StringComparison.InvariantCultureIgnoreCase));
            }
        }).ConfigureAwait(false);
    }
}
