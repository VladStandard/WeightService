// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System;
using WsStorageCore.Common;

namespace WsWebApiTerra1000Tests.Controllers;

[TestFixture]
public sealed class NomenclatureControllerTests
{
    [Test]
    public void GetListNomenclature_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in new WsWebRequestTerra1000().GetListNomenclature(WsSqlEnumServerType.All))
            {
                foreach (long id in WsServiceUtilsTests.GetListNomenclatureId)
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
                foreach (long id in WsServiceUtilsTests.GetListNomenclatureId)
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
