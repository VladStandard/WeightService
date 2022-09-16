// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using WebApiCore.Common;

namespace WebApiTerra1000Tests.Controllers;

[TestFixture]
internal class TestControllerTests
{
    [Test]
    public void GetProdInfoV1_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetProdListUrlInfoV1)
            {
                await GetInfoAsync(url);
            }
        });
    }

    [Test]
    public void GetDevInfoV1_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetDevListUrlInfoV1)
            {
                await GetInfoAsync(url);
            }
        });
    }

    [Test]
    public void GetProdInfoV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetProdListUrlInfoV2)
            {
                await GetInfoAsync(url);
            }
        });
    }

    [Test]
    public void GetDevInfoV2_Execute_DoesNotThrow()
    {
        Assert.DoesNotThrowAsync(async () =>
        {
            foreach (string url in TestsUtils.GetDevListUrlInfoV2)
            {
                await GetInfoAsync(url);
            }
        });
    }

    private async Task GetInfoAsync(string url)
    {
        RestClientOptions options = new(url)
        {
            UseDefaultCredentials = true,
            ThrowOnAnyError = true,
            MaxTimeout = 60_000,
				RemoteCertificateValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
			};
			using RestClient client = new(options);
        RestRequest request = new RestRequest()
            .AddQueryParameter("format", "json");
        RestResponse response = await client.GetAsync(request);

        TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
        TestContext.WriteLine($"{nameof(response)}: {response.Content}");
        Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
        if (!string.IsNullOrEmpty(response.Content))
        {
            ServiceInfoEntity? serviceInfo = JsonConvert.DeserializeObject<ServiceInfoEntity>(response.Content);
            Assert.IsTrue(serviceInfo != null);
            Assert.IsTrue(serviceInfo?.App.StartsWith("WebApi", System.StringComparison.InvariantCultureIgnoreCase));
        }
        TestContext.WriteLine();
    }
}
