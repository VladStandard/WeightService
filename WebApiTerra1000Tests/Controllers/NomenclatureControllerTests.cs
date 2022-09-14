// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com

using DataCoreTests;
using NUnit.Framework;
using RestSharp;
using System.Net;
using System.Threading.Tasks;

namespace DataProjectsCoreTests.DAL
{
    [TestFixture]
    internal class NomenclatureControllerTests
    {
        //[Test]
        //public void GetNomenclatureCodeV1_Execute_DoesNotThrow()
        //{
        //    Assert.DoesNotThrowAsync(async () =>
        //    {
        //        foreach (string url in TestsUtils.GetListUrlNomenclatureV1)
        //        {
        //            foreach (string code in TestsUtils.GetListNomenclatureCode)
        //            {
        //                await GetNomenclatureAsync(url, code, null);
        //            }
        //        }
        //    });
        //    TestContext.WriteLine();
        //}

        //[Test]
        //public void GetNomenclatureCodeV2_Execute_DoesNotThrow()
        //{
        //    Assert.DoesNotThrowAsync(async () =>
        //    {
        //        foreach (string url in TestsUtils.GetListUrlNomenclatureV2)
        //        {
        //            foreach (string code in TestsUtils.GetListNomenclatureCode)
        //            {
        //                await GetNomenclatureAsync(url, code, null);
        //            }
        //        }
        //    });
        //}

        [Test]
        public void GetNomenclatureIdV1_Execute_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                foreach (string url in TestsUtils.GetListUrlNomenclatureV1)
                {
                    if (url.Contains("-dev")) continue;
                    foreach (long id in TestsUtils.GetListNomenclatureId)
                    {
                        await GetNomenclatureAsync(url, null, id);
                    }
                }
            });
            TestContext.WriteLine();
        }

        [Test]
        public void GetNomenclatureIdV2_Execute_DoesNotThrow()
        {
            Assert.DoesNotThrowAsync(async () =>
            {
                foreach (string url in TestsUtils.GetListUrlNomenclatureV2)
                {
                    if (url.Contains("-dev")) continue;
                    foreach (long id in TestsUtils.GetListNomenclatureId)
                    {
                        await GetNomenclatureAsync(url, null, id);
                    }
                }
            });
        }

        private async Task GetNomenclatureAsync(string url, string? code, long? id)
        {
			RestClientOptions options = new(url)
            {
                UseDefaultCredentials = true,
                ThrowOnAnyError = true,
                Timeout = 60_000,
            };
            using RestClient client = new(options);
            RestRequest request = new();
            if (code != null)
                request.AddQueryParameter("code", code);
            else if (id != null)
                request.AddQueryParameter("id", id.ToString());
            RestResponse response = await client.GetAsync(request);

            TestContext.WriteLine($"{nameof(response.ResponseUri)}: {response.ResponseUri}");
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            if (!string.IsNullOrEmpty(response.Content))
            {
                if (code != null)
                    Assert.IsTrue(response.Content.Contains($"Code=\"{code}\"", System.StringComparison.InvariantCultureIgnoreCase));
                else if (id != null)
                    Assert.IsTrue(response.Content.Contains($"ID=\"{id}\"", System.StringComparison.InvariantCultureIgnoreCase));
            }
            TestContext.WriteLine();
        }
    }
}
