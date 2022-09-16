// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using RestSharp;

namespace WebApiTerra1000Tests.Controllers;

[TestFixture]
internal class ContragentControllerTests
{
    //[Test]
    //public void GetContragentCodeV1_Execute_DoesNotThrow()
    //{
    //    Assert.DoesNotThrowAsync(async () =>
    //    {
    //        foreach (string url in TestsUtils.GetListUrlContragentV1)
    //        {
    //            foreach (string code in TestsUtils.GetListContragentCode)
    //            {
    //                await GetContragentAsync(url, code, null);
    //            }
    //        }
    //    });
    //    TestContext.WriteLine();
    //}

    //[Test]
    //public void GetContragentCodeV2_Execute_DoesNotThrow()
    //{
    //    Assert.DoesNotThrowAsync(async () =>
    //    {
    //        foreach (string url in TestsUtils.GetListUrlContragentV2)
    //        {
    //            foreach (string code in TestsUtils.GetListContragentCode)
    //            {
    //                await GetContragentAsync(url, code, null);
    //            }
    //        }
    //    });
    //}

    [Test]
    public void GetContragentIdV1_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetListUrlContragentV1)
            {
                if (url.Contains("-dev")) continue;
                foreach (long id in TestsUtils.GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                }
            }
        });
        TestContext.WriteLine();
    }

    [Test]
    public void GetContragentIdV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetListUrlContragentV2)
            {
                if (url.Contains("-dev")) continue;
                foreach (long id in TestsUtils.GetListContragentId)
                {
                    await GetContragentAsync(url, null, id);
                }
            }
        });
    }

    private async Task GetContragentAsync(string url, string? code, long? id)
    {
			RestClientOptions options = new(url)
        {
            UseDefaultCredentials = true,
            ThrowOnAnyError = true,
            MaxTimeout = 60_000,
				RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
			};
        using RestClient client = new(options);
        RestRequest request = new();
        if (code is not null)
            request.AddQueryParameter("code", code);
        if (id is not null)
            request.AddQueryParameter("id", id.ToString());
        RestResponse response = await client.GetAsync(request);

        TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        if (!string.IsNullOrEmpty(response.Content))
        {
            if (code is not null)
                Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", System.StringComparison.InvariantCultureIgnoreCase));
            if (id is not null)
                Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", System.StringComparison.InvariantCultureIgnoreCase));
        }
        TestContext.WriteLine();
    }
}
