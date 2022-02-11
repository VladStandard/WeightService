// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCoreTests;
using NUnit.Framework;
using RestSharp;

namespace DataProjectsCoreTests.DAL
{
    [TestFixture]
    internal class TestControllerTests
    {
        [Test]
        public void GetInfo_Execute_DoesNotThrow()
        {
            TestsUtils.MethodStart();

            Assert.DoesNotThrowAsync(async () =>
            {
                RestSharp.RestClientOptions options = new(TestsUtils.GetUrlDev) {
                    UseDefaultCredentials = true,
                    ThrowOnAnyError = true,
                    Timeout = 60_000,
                };
                RestSharp.RestClient client = new(options);
                //client.AddDefaultHeader(KnownHeaders.Accept, "application/vnd.github.v3+json");
                //RestRequest request = new RestSharp.RestRequest().AddQueryParameter("format", "json");//.AddJsonBody(someObject);
                RestRequest request = new RestSharp.RestRequest().AddQueryParameter("format", "json");//.AddJsonBody(someObject);
                //var response = await client.PostAsync<MyResponse>(request, cancellationToken);
                //var response = await client.PostAsync<MyResponse>(request, cancellationToken);
                RestResponse response = await client.PostAsync(request);

                Assert.AreEqual(1, 1);
            });
            TestContext.WriteLine();

            TestsUtils.MethodComplete();
        }
    }
}
