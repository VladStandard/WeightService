// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCoreTests;
using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;
using WebApiTerra1000.Common;

namespace DataProjectsCoreTests.DAL
{
    [TestFixture]
    internal class TestControllerTests
    {
        [Test]
        public void GetInfoV1_Execute_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                foreach (string url in TestsUtils.GetListUrlInfoV1)
                {
                    await GetInfoAsync(url);
                }
            });
        }

        [Test]
        public void GetInfoV2_Execute_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                foreach (string url in TestsUtils.GetListUrlInfoV2)
                {
                    await GetInfoAsync(url);
                }
            });
        }

        private async Task GetInfoAsync(string url)
        {
            RestSharp.RestClientOptions options = new(url)
            {
                UseDefaultCredentials = true,
                ThrowOnAnyError = true,
                Timeout = 60_000,
            };
            RestSharp.RestClient client = new(options);
            RestRequest request = new RestSharp.RestRequest()
                .AddQueryParameter("format", "json");
            RestResponse response = await client.GetAsync(request);

            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            TestContext.WriteLine($"{nameof(response)}: {response.Content}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            if (!string.IsNullOrEmpty(response.Content))
            {
                ServiceInfoEntity? serviceInfo = JsonConvert.DeserializeObject<ServiceInfoEntity>(response.Content);
                Assert.IsTrue(serviceInfo != null);
                Assert.AreEqual(serviceInfo?.App, "WebApiTerra1000");
            }
            TestContext.WriteLine();
        }
    }
}
